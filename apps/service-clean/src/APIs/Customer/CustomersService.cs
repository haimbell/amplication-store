using ServiceClean.Infrastructure;

namespace ServiceClean.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(ServiceCleanDbContext context)
        : base(context) { }
}
