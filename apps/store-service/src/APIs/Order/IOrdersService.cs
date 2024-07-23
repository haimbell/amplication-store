using StoreService.APIs.Common;
using StoreService.APIs.Dtos;

namespace StoreService.APIs;

public interface IOrdersService
{
    /// <summary>
    /// Create one Order
    /// </summary>
    public Task<Order> CreateOrder(OrderCreateInput order);

    /// <summary>
    /// Find many Orders
    /// </summary>
    public Task<List<Order>> Orders(OrderFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Order
    /// </summary>
    public Task<Order> Order(OrderWhereUniqueInput uniqueId);

    /// <summary>
    /// Connect multiple OrderItems records to Order
    /// </summary>
    public Task ConnectOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    );

    /// <summary>
    /// Disconnect multiple OrderItems records from Order
    /// </summary>
    public Task DisconnectOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    );

    /// <summary>
    /// Find multiple OrderItems records for Order
    /// </summary>
    public Task<List<OrderItem>> FindOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemFindManyArgs OrderItemFindManyArgs
    );

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    public Task<Customer> GetCustomer(OrderWhereUniqueInput uniqueId);

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    public Task<MetadataDto> OrdersMeta(OrderFindManyArgs findManyArgs);

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    public Task UpdateOrderItems(
        OrderWhereUniqueInput uniqueId,
        OrderItemWhereUniqueInput[] orderItemsId
    );

    /// <summary>
    /// Update one Order
    /// </summary>
    public Task UpdateOrder(OrderWhereUniqueInput uniqueId, OrderUpdateInput updateDto);
}
