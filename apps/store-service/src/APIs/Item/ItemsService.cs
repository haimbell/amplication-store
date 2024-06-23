using StoreService.Infrastructure;

namespace StoreService.APIs;

public class ItemsService : ItemsServiceBase
{
    public ItemsService(StoreServiceDbContext context)
        : base(context) { }
}
