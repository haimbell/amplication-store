using Microsoft.EntityFrameworkCore;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;
using StoreService.APIs.Extensions;
using StoreService.Infrastructure;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs;

public abstract class OrderItemsServiceBase : IOrderItemsService
{
    protected readonly StoreServiceDbContext _context;

    public OrderItemsServiceBase(StoreServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one OrderItems
    /// </summary>
    public async Task<OrderItem> CreateOrderItem(OrderItemCreateInput createDto)
    {
        var orderItem = new OrderItemDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            orderItem.Id = createDto.Id;
        }
        if (createDto.Item != null)
        {
            orderItem.Item = await _context
                .Items.Where(item => createDto.Item.Id == item.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Order != null)
        {
            orderItem.Order = await _context
                .Orders.Where(order => createDto.Order.Id == order.Id)
                .FirstOrDefaultAsync();
        }

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<OrderItemDbModel>(orderItem.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one OrderItems
    /// </summary>
    public async Task DeleteOrderItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context.OrderItems.FindAsync(uniqueId.Id);
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    public async Task<List<OrderItem>> OrderItems(OrderItemFindManyArgs findManyArgs)
    {
        var orderItems = await _context
            .OrderItems.Include(x => x.Item)
            .Include(x => x.Order)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return orderItems.ConvertAll(orderItem => orderItem.ToDto());
    }

    /// <summary>
    /// Get one OrderItems
    /// </summary>
    public async Task<OrderItem> OrderItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItems = await this.OrderItems(
            new OrderItemFindManyArgs { Where = new OrderItemWhereInput { Id = uniqueId.Id } }
        );
        var orderItem = orderItems.FirstOrDefault();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }

        return orderItem;
    }

    /// <summary>
    /// Get a Item record for OrderItems
    /// </summary>
    public async Task<Item> GetItem(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context
            .OrderItems.Where(orderItem => orderItem.Id == uniqueId.Id)
            .Include(orderItem => orderItem.Item)
            .FirstOrDefaultAsync();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }
        return orderItem.Item.ToDto();
    }

    /// <summary>
    /// Get a Order record for OrderItems
    /// </summary>
    public async Task<Order> GetOrder(OrderItemWhereUniqueInput uniqueId)
    {
        var orderItem = await _context
            .OrderItems.Where(orderItem => orderItem.Id == uniqueId.Id)
            .Include(orderItem => orderItem.Order)
            .FirstOrDefaultAsync();
        if (orderItem == null)
        {
            throw new NotFoundException();
        }
        return orderItem.Order.ToDto();
    }

    /// <summary>
    /// Meta data about OrderItems records
    /// </summary>
    public async Task<MetadataDto> OrderItemsMeta(OrderItemFindManyArgs findManyArgs)
    {
        var count = await _context.OrderItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one OrderItems
    /// </summary>
    public async Task UpdateOrderItem(
        OrderItemWhereUniqueInput uniqueId,
        OrderItemUpdateInput updateDto
    )
    {
        var orderItem = updateDto.ToModel(uniqueId);

        _context.Entry(orderItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.OrderItems.Any(e => e.Id == orderItem.Id))
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
