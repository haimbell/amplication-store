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
    public async Task<ActionResult<Order>> CreateOrder(OrderCreateInput input)
    {
        var order = await _service.CreateOrder(input);

        return CreatedAtAction(nameof(Order), new { id = order.Id }, order);
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Order>>> Orders([FromQuery()] OrderFindManyArgs filter)
    {
        return Ok(await _service.Orders(filter));
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Order>> Order([FromRoute()] OrderWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Order(uniqueId);
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
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        try
        {
            await _service.ConnectOrderItems(uniqueId, orderItemsId);
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
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromBody()] OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        try
        {
            await _service.DisconnectOrderItems(uniqueId, orderItemsId);
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
    public async Task<ActionResult<List<OrderItem>>> FindOrderItems(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] OrderItemFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindOrderItems(uniqueId, filter));
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
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] OrderWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Meta data about Order records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrdersMeta([FromQuery()] OrderFindManyArgs filter)
    {
        return Ok(await _service.OrdersMeta(filter));
    }

    /// <summary>
    /// Update multiple OrderItems records for Order
    /// </summary>
    [HttpPatch("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrderItems(
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromBody()] OrderItemWhereUniqueInput[] orderItemsId
    )
    {
        try
        {
            await _service.UpdateOrderItems(uniqueId, orderItemsId);
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
        [FromRoute()] OrderWhereUniqueInput uniqueId,
        [FromQuery()] OrderUpdateInput orderUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrder(uniqueId, orderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
