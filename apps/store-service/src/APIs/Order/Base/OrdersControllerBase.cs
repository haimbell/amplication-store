using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;

namespace StoreService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrdersControllerBase : ControllerBase
{
    protected readonly IOrdersService _service;

    public OrdersControllerBase(IOrdersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateInput input)
    {
        var order = await _service.CreateOrder(input);

        return CreatedAtAction(nameof(Order), new { id = order.Id }, order);
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<OrderDto>>> Orders([FromQuery()] OrderFindMany filter)
    {
        return Ok(await _service.Orders(filter));
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<OrderDto>> Order([FromRoute()] OrderIdDto idDto)
    {
        try
        {
            return await _service.Order(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple OrderItems records to Order
    /// </summary>
    [HttpPost("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectOrderItems(
        [FromRoute()] OrderIdDto idDto,
        [FromQuery()] OrderItemIdDto[] orderItemsId
    )
    {
        try
        {
            await _service.ConnectOrderItems(idDto, orderItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple OrderItems records from Order
    /// </summary>
    [HttpDelete("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectOrderItems(
        [FromRoute()] OrderIdDto idDto,
        [FromBody()] OrderItemIdDto[] orderItemsId
    )
    {
        try
        {
            await _service.DisconnectOrderItems(idDto, orderItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple OrderItems records for Order
    /// </summary>
    [HttpGet("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<OrderItemDto>>> FindOrderItems(
        [FromRoute()] OrderIdDto idDto,
        [FromQuery()] OrderItemFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindOrderItems(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    [HttpGet("{Id}/customers")]
    public async Task<ActionResult<List<CustomerDto>>> GetCustomer([FromRoute()] OrderIdDto idDto)
    {
        var customer = await _service.GetCustomer(idDto);
        return Ok(customer);
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrdersMeta([FromQuery()] OrderFindMany filter)
    {
        return Ok(await _service.OrdersMeta(filter));
    }

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    [HttpPatch("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrderItems(
        [FromRoute()] OrderIdDto idDto,
        [FromBody()] OrderItemIdDto[] orderItemsId
    )
    {
        try
        {
            await _service.UpdateOrderItems(idDto, orderItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrder(
        [FromRoute()] OrderIdDto idDto,
        [FromQuery()] OrderUpdateInput orderUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrder(idDto, orderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
