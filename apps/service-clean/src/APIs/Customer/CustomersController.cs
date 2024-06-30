using Microsoft.AspNetCore.Mvc;

namespace ServiceClean.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
