using Microsoft.AspNetCore.Mvc;

namespace StoreService.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
