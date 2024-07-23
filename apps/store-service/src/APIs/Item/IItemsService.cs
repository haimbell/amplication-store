using StoreService.APIs.Common;
using StoreService.APIs.Dtos;

namespace StoreService.APIs;

public interface IItemsService
{
    /// <summary>
    /// Create one Item
    /// </summary>
    public Task<Item> CreateItem(ItemCreateInput item);

    /// <summary>
    /// Delete one Item
    /// </summary>
    public Task DeleteItem(ItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Items
    /// </summary>
    public Task<List<Item>> Items(ItemFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Item
    /// </summary>
    public Task<Item> Item(ItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple OrderItems records to Item
    /// </summary>
    public Task ConnectOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    );

    /// <summary>
    /// Disconnect multiple OrderItems records from Item
    /// </summary>
    public Task DisconnectOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    );

    /// <summary>
    /// Find multiple OrderItems records for Item
    /// </summary>
    public Task<List<OrderItem>> FindOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemFindManyArgs OrderItemFindManyArgs
    );

    /// <summary>
    /// Meta data about Item records
    /// </summary>
    public Task<MetadataDto> ItemsMeta(ItemFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple OrderItems records for Item
    /// </summary>
    public Task UpdateOrderItems(
        ItemWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    );

    /// <summary>
    /// Update one Item
    /// </summary>
    public Task UpdateItem(ItemWhereUniqueInput uniqueId, ItemUpdateInput updateDto);
}
