using StoreService.Infrastructure;

namespace StoreService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(StoreServiceDbContext context)
        : base(context) { }
}
