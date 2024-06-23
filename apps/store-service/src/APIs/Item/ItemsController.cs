using Microsoft.AspNetCore.Mvc;

namespace StoreService.APIs;

[ApiController()]
public class ItemsController : ItemsControllerBase
{
    public ItemsController(IItemsService service)
        : base(service) { }
}
