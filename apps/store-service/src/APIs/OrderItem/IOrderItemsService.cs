using StoreService.APIs.Common;
using StoreService.APIs.Dtos;

namespace StoreService.APIs;

public interface IOrderItemsService
{
    /// <summary>
    /// Create one OrderItems
    /// </summary>
    public Task<OrderItemDto> CreateOrderItem(OrderItemCreateInput orderitemDto);

    /// <summary>
    /// Delete one OrderItems
    /// </summary>
    public Task DeleteOrderItem(OrderItemIdDto idDto);

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    public Task<List<OrderItemDto>> OrderItems(OrderItemFindMany findManyArgs);

    /// <summary>
    /// Get one OrderItems
    /// </summary>
    public Task<OrderItemDto> OrderItem(OrderItemIdDto idDto);

    /// <summary>
    /// Get a Item record for OrderItems
    /// </summary>
    public Task<ItemDto> GetItem(OrderItemIdDto idDto);

    /// <summary>
    /// Get a Order record for OrderItems
    /// </summary>
    public Task<OrderDto> GetOrder(OrderItemIdDto idDto);

    /// <summary>
    /// Meta data about OrderItems records
    /// </summary>
    public Task<MetadataDto> OrderItemsMeta(OrderItemFindMany findManyArgs);

    /// <summary>
    /// Update one OrderItems
    /// </summary>
    public Task UpdateOrderItem(OrderItemIdDto idDto, OrderItemUpdateInput updateDto);
}
