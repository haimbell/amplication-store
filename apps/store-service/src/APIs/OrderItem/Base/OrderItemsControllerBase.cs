using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;

namespace StoreService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrderItemsControllerBase : ControllerBase
{
    protected readonly IOrderItemsService _service;

    public OrderItemsControllerBase(IOrderItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one OrderItems
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItemCreateInput input)
    {
        var orderItem = await _service.CreateOrderItem(input);

        return CreatedAtAction(nameof(OrderItem), new { id = orderItem.Id }, orderItem);
    }

    /// <summary>
    /// Delete one OrderItems
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteOrderItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteOrderItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many OrderItems
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<OrderItem>>> OrderItems(
        [FromQuery()] OrderItemFindManyArgs filter
    )
    {
        return Ok(await _service.OrderItems(filter));
    }

    /// <summary>
    /// Get one OrderItems
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<OrderItem>> OrderItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.OrderItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Item record for OrderItems
    /// </summary>
    [HttpGet("{Id}/items")]
    public async Task<ActionResult<List<Item>>> GetItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        var item = await _service.GetItem(uniqueId);
        return Ok(item);
    }

    /// <summary>
    /// Get a Order record for OrderItems
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<Order>>> GetOrder(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId
    )
    {
        var order = await _service.GetOrder(uniqueId);
        return Ok(order);
    }

    /// <summary>
    /// Meta data about OrderItems records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrderItemsMeta(
        [FromQuery()] OrderItemFindManyArgs filter
    )
    {
        return Ok(await _service.OrderItemsMeta(filter));
    }

    /// <summary>
    /// Update one OrderItems
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrderItem(
        [FromRoute()] OrderItemWhereUniqueInput uniqueId,
        [FromQuery()] OrderItemUpdateInput orderItemUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrderItem(uniqueId, orderItemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
