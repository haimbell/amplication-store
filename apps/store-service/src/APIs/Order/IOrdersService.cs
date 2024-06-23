using StoreService.APIs.Common;
using StoreService.APIs.Dtos;

namespace StoreService.APIs;

public interface IOrdersService
{
    /// <summary>
    /// Create one Order
    /// </summary>
    public Task<OrderDto> CreateOrder(OrderCreateInput orderDto);

    /// <summary>
    /// Find many Orders
    /// </summary>
    public Task<List<OrderDto>> Orders(OrderFindMany findManyArgs);

    /// <summary>
    /// Get one Order
    /// </summary>
    public Task<OrderDto> Order(OrderIdDto idDto);

    /// <summary>
    /// Connect multiple OrderItems records to Order
    /// </summary>
    public Task ConnectOrderItems(OrderIdDto idDto, OrderItemIdDto[] orderItemsId);

    /// <summary>
    /// Disconnect multiple OrderItems records from Order
    /// </summary>
    public Task DisconnectOrderItems(OrderIdDto idDto, OrderItemIdDto[] orderItemsId);

    /// <summary>
    /// Find multiple OrderItems records for Order
    /// </summary>
    public Task<List<OrderItemDto>> FindOrderItems(
        OrderIdDto idDto,
        OrderItemFindMany OrderItemFindMany
    );

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    public Task<CustomerDto> GetCustomer(OrderIdDto idDto);

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    public Task<MetadataDto> OrdersMeta(OrderFindMany findManyArgs);

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    public Task UpdateOrderItems(OrderIdDto idDto, OrderItemIdDto[] orderItemsId);

    /// <summary>
    /// Update one Order
    /// </summary>
    public Task UpdateOrder(OrderIdDto idDto, OrderUpdateInput updateDto);
}
