using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;

namespace StoreService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Connect multiple Orders records to Customer
    /// </summary>
    [HttpPost("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] OrderIdDto[] ordersId
    )
    {
        try
        {
            await _service.ConnectOrders(idDto, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Orders records from Customer
    /// </summary>
    [HttpDelete("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] CustomerIdDto idDto,
        [FromBody()] OrderIdDto[] ordersId
    )
    {
        try
        {
            await _service.DisconnectOrders(idDto, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Orders records for Customer
    /// </summary>
    [HttpGet("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<OrderDto>>> FindOrders(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] OrderFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindOrders(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindMany filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Update multiple Orders records for Customer
    /// </summary>
    [HttpPatch("{Id}/orders")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] CustomerIdDto idDto,
        [FromBody()] OrderIdDto[] ordersId
    )
    {
        try
        {
            await _service.UpdateOrders(idDto, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerIdDto idDto)
    {
        try
        {
            await _service.DeleteCustomer(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<CustomerDto>>> Customers(
        [FromQuery()] CustomerFindMany filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<CustomerDto>> Customer([FromRoute()] CustomerIdDto idDto)
    {
        try
        {
            return await _service.Customer(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(idDto, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
