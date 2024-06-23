using Microsoft.EntityFrameworkCore;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;
using StoreService.APIs.Extensions;
using StoreService.Infrastructure;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs;

public abstract class OrdersServiceBase : IOrdersService
{
    protected readonly StoreServiceDbContext _context;

    public OrdersServiceBase(StoreServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    public async Task<OrderDto> CreateOrder(OrderCreateInput createDto)
    {
        var order = new Order
        {
            CreatedAt = createDto.CreatedAt,
            Status = createDto.Status,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            order.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            order.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.OrderItems != null)
        {
            order.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    createDto.OrderItems.Select(t => t.Id).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Order>(order.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    public async Task<List<OrderDto>> Orders(OrderFindMany findManyArgs)
    {
        var orders = await _context
            .Orders.Include(x => x.Customer)
            .Include(x => x.OrderItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orders.ConvertAll(order => order.ToDto());
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    public async Task<OrderDto> Order(OrderIdDto idDto)
    {
        var orders = await this.Orders(
            new OrderFindMany { Where = new OrderWhereInput { Id = idDto.Id } }
        );
        var order = orders.FirstOrDefault();
        if (order == null)
        {
            throw new NotFoundException();
        }

        return order;
    }

    /// <summary>
    /// Connect multiple OrderItems records to Order
    /// </summary>
    public async Task ConnectOrderItems(OrderIdDto idDto, OrderItemIdDto[] orderItemsId)
    {
        var order = await _context
            .Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(t => orderItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (orderItems.Count == 0)
        {
            throw new NotFoundException();
        }

        var orderItemsToConnect = orderItems.Except(order.OrderItems);

        foreach (var orderItem in orderItemsToConnect)
        {
            order.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple OrderItems records from Order
    /// </summary>
    public async Task DisconnectOrderItems(OrderIdDto idDto, OrderItemIdDto[] orderItemsId)
    {
        var order = await _context
            .Orders.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(t => orderItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var orderItem in orderItems)
        {
            order.OrderItems?.Remove(orderItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple OrderItems records for Order
    /// </summary>
    public async Task<List<OrderItemDto>> FindOrderItems(
        OrderIdDto idDto,
        OrderItemFindMany orderFindMany
    )
    {
        var orderItems = await _context
            .OrderItems.Where(m => m.OrderId == idDto.Id)
            .ApplyWhere(orderFindMany.Where)
            .ApplySkip(orderFindMany.Skip)
            .ApplyTake(orderFindMany.Take)
            .ApplyOrderBy(orderFindMany.SortBy)
            .ToListAsync();

        return orderItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    public async Task<CustomerDto> GetCustomer(OrderIdDto idDto)
    {
        var order = await _context
            .Orders.Where(order => order.Id == idDto.Id)
            .Include(order => order.Customer)
            .FirstOrDefaultAsync();
        if (order == null)
        {
            throw new NotFoundException();
        }
        return order.Customer.ToDto();
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    public async Task<MetadataDto> OrdersMeta(OrderFindMany findManyArgs)
    {
        var count = await _context.Orders.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    public async Task UpdateOrderItems(OrderIdDto idDto, OrderItemIdDto[] orderItemsId)
    {
        var order = await _context
            .Orders.Include(t => t.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (order == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(a => orderItemsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (orderItems.Count == 0)
        {
            throw new NotFoundException();
        }

        order.OrderItems = orderItems;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    public async Task UpdateOrder(OrderIdDto idDto, OrderUpdateInput updateDto)
    {
        var order = updateDto.ToModel(idDto);

        if (updateDto.OrderItems != null)
        {
            order.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    updateDto.OrderItems.Select(t => t.Id).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        _context.Entry(order).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Orders.Any(e => e.Id == order.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
