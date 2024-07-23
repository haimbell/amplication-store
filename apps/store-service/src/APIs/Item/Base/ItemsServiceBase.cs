using Microsoft.EntityFrameworkCore;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;
using StoreService.APIs.Extensions;
using StoreService.Infrastructure;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs;

public abstract class ItemsServiceBase : IItemsService
{
    protected readonly StoreServiceDbContext _context;

    public ItemsServiceBase(StoreServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Item
    /// </summary>
    public async Task<Item> CreateItem(ItemCreateInput createDto)
    {
        var item = new ItemDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name,
            Price = createDto.Price
        };

        if (createDto.Id != null)
        {
            item.Id = createDto.Id;
        }
        if (createDto.OrderItems != null)
        {
            item.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    createDto.OrderItems.Select(t => t.Id).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ItemDbModel>(item.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Item
    /// </summary>
    public async Task DeleteItem(ItemWhereUniqueInput uniqueId)
    {
        var item = await _context.Items.FindAsync(uniqueId.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Items
    /// </summary>
    public async Task<List<Item>> Items(ItemFindManyArgs findManyArgs)
    {
        var items = await _context
            .Items.Include(x => x.OrderItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return items.ConvertAll(item => item.ToDto());
    }

    /// <summary>
    /// Get one Item
    /// </summary>
    public async Task<Item> Item(ItemWhereUniqueInput uniqueId)
    {
        var items = await this.Items(
            new ItemFindManyArgs { Where = new ItemWhereInput { Id = uniqueId.Id } }
        );
        var item = items.FirstOrDefault();
        if (item == null)
        {
            throw new NotFoundException();
        }

        return item;
    }

    /// <summary>
    /// Connect multiple OrderItems records to Item
    /// </summary>
    public async Task ConnectOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        var item = await _context
            .Items.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (item == null)
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

        var orderItemsToConnect = orderItems.Except(item.OrderItems);

        foreach (var orderItem in orderItemsToConnect)
        {
            item.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple OrderItems records from Item
    /// </summary>
    public async Task DisconnectOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        var item = await _context
            .Items.Include(x => x.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (item == null)
        {
            throw new NotFoundException();
        }

        var orderItems = await _context
            .OrderItems.Where(t => orderItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var orderItem in orderItems)
        {
            item.OrderItems?.Remove(orderItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple OrderItems records for Item
    /// </summary>
    public async Task<List<OrderItem>> FindOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemFindManyArgs itemFindManyArgs
    )
    {
        var orderItems = await _context
            .OrderItems.Where(m => m.ItemId == uniqueId.Id)
            .ApplyWhere(itemFindManyArgs.Where)
            .ApplySkip(itemFindManyArgs.Skip)
            .ApplyTake(itemFindManyArgs.Take)
            .ApplyOrderBy(itemFindManyArgs.SortBy)
            .ToListAsync();

        return orderItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Meta data about Item records
    /// </summary>
    public async Task<MetadataDto> ItemsMeta(ItemFindManyArgs findManyArgs)
    {
        var count = await _context.Items.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Update multiple OrderItems records for Item
    /// </summary>
    public async Task UpdateOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        var item = await _context
            .Items.Include(t => t.OrderItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (item == null)
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

        item.OrderItems = orderItems;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one Item
    /// </summary>
    public async Task UpdateItem(ItemWhereUniqueInput uniqueId, ItemUpdateInput updateDto)
    {
        var item = updateDto.ToModel(uniqueId);

        if (updateDto.OrderItems != null)
        {
            item.OrderItems = await _context
                .OrderItems.Where(orderItem =>
                    updateDto.OrderItems.Select(t => t).Contains(orderItem.Id)
                )
                .ToListAsync();
        }

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Items.Any(e => e.Id == item.Id))
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
