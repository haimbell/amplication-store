using StoreService.Infrastructure;

namespace StoreService.APIs;

public class OrderItemsService : OrderItemsServiceBase
{
    public OrderItemsService(StoreServiceDbContext context)
        : base(context) { }
}
