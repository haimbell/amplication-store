using StoreService.APIs.Common;
using StoreService.APIs.Dtos;

namespace StoreService.APIs;

public interface IItemsService
{
    /// <summary>
    /// Create one Item
    /// </summary>
    public Task<ItemDto> CreateItem(ItemCreateInput itemDto);

    /// <summary>
    /// Delete one Item
    /// </summary>
    public Task DeleteItem(ItemIdDto idDto);

    /// <summary>
    /// Find many Items
    /// </summary>
    public Task<List<ItemDto>> Items(ItemFindMany findManyArgs);

    /// <summary>
    /// Get one Item
    /// </summary>
    public Task<ItemDto> Item(ItemIdDto idDto);

    /// <summary>
    /// Connect multiple OrderItems records to Item
    /// </summary>
    public Task ConnectOrderItems(ItemIdDto idDto, OrderItemIdDto[] orderItemsId);

    /// <summary>
    /// Disconnect multiple OrderItems records from Item
    /// </summary>
    public Task DisconnectOrderItems(ItemIdDto idDto, OrderItemIdDto[] orderItemsId);

    /// <summary>
    /// Find multiple OrderItems records for Item
    /// </summary>
    public Task<List<OrderItemDto>> FindOrderItems(
        ItemIdDto idDto,
        OrderItemFindMany OrderItemFindMany
    );

    /// <summary>
    /// Meta data about Item records
    /// </summary>
    public Task<MetadataDto> ItemsMeta(ItemFindMany findManyArgs);

    /// <summary>
    /// Update multiple OrderItems records for Item
    /// </summary>
    public Task UpdateOrderItems(ItemIdDto idDto, OrderItemIdDto[] orderItemsId);

    /// <summary>
    /// Update one Item
    /// </summary>
    public Task UpdateItem(ItemIdDto idDto, ItemUpdateInput updateDto);
}
