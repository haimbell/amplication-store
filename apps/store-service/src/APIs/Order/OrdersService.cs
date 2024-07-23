using StoreService.Infrastructure;

namespace StoreService.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(StoreServiceDbContext context)
        : base(context) { }
}
