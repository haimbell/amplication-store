using StoreService.APIs.Common;
using StoreService.APIs.Dtos;

namespace StoreService.APIs;

public interface IOrderItemsService
{
    /// <summary>
    /// Create one OrderItems
    /// </summary>
    public Task<OrderItem> CreateOrderItem(OrderItemCreateInput orderitem);

    /// <summary>
    /// Delete one OrderItems
    /// </summary>
    public Task DeleteOrderItem(OrderItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    public Task<List<OrderItem>> OrderItems(OrderItemFindManyArgs findManyArgs);

    /// <summary>
    /// Get one OrderItems
    /// </summary>
    public Task<OrderItem> OrderItem(OrderItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Item record for OrderItems
    /// </summary>
    public Task<Item> GetItem(OrderItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a Order record for OrderItems
    /// </summary>
    public Task<Order> GetOrder(OrderItemWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about OrderItems records
    /// </summary>
    public Task<MetadataDto> OrderItemsMeta(OrderItemFindManyArgs findManyArgs);

    /// <summary>
    /// Update one OrderItems
    /// </summary>
    public Task UpdateOrderItem(OrderItemWhereUniqueInput uniqueId, OrderItemUpdateInput updateDto);
}
