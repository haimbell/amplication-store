using Microsoft.AspNetCore.Mvc;

namespace StoreService.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
