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
    public async Task<OrderItemDto> CreateOrderItem(OrderItemCreateInput createDto)
    {
        var orderItem = new OrderItem
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

        var result = await _context.FindAsync<OrderItem>(orderItem.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one OrderItems
    /// </summary>
    public async Task DeleteOrderItem(OrderItemIdDto idDto)
    {
        var orderItem = await _context.OrderItems.FindAsync(idDto.Id);
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
    public async Task<List<OrderItemDto>> OrderItems(OrderItemFindMany findManyArgs)
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
    public async Task<OrderItemDto> OrderItem(OrderItemIdDto idDto)
    {
        var orderItems = await this.OrderItems(
            new OrderItemFindMany { Where = new OrderItemWhereInput { Id = idDto.Id } }
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
    public async Task<ItemDto> GetItem(OrderItemIdDto idDto)
    {
        var orderItem = await _context
            .OrderItems.Where(orderItem => orderItem.Id == idDto.Id)
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
    public async Task<OrderDto> GetOrder(OrderItemIdDto idDto)
    {
        var orderItem = await _context
            .OrderItems.Where(orderItem => orderItem.Id == idDto.Id)
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
    public async Task<MetadataDto> OrderItemsMeta(OrderItemFindMany findManyArgs)
    {
        var count = await _context.OrderItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update one OrderItems
    /// </summary>
    public async Task UpdateOrderItem(OrderItemIdDto idDto, OrderItemUpdateInput updateDto)
    {
        var orderItem = updateDto.ToModel(idDto);

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
