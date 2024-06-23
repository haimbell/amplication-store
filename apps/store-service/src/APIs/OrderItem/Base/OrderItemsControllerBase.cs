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
    public async Task<ActionResult<OrderItemDto>> CreateOrderItem(OrderItemCreateInput input)
    {
        var orderItem = await _service.CreateOrderItem(input);

        return CreatedAtAction(nameof(OrderItem), new { id = orderItem.Id }, orderItem);
    }

    /// <summary>
    /// Delete one OrderItems
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteOrderItem([FromRoute()] OrderItemIdDto idDto)
    {
        try
        {
            await _service.DeleteOrderItem(idDto);
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
    public async Task<ActionResult<List<OrderItemDto>>> OrderItems(
        [FromQuery()] OrderItemFindMany filter
    )
    {
        return Ok(await _service.OrderItems(filter));
    }

    /// <summary>
    /// Get one OrderItems
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<OrderItemDto>> OrderItem([FromRoute()] OrderItemIdDto idDto)
    {
        try
        {
            return await _service.OrderItem(idDto);
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
    public async Task<ActionResult<List<ItemDto>>> GetItem([FromRoute()] OrderItemIdDto idDto)
    {
        var item = await _service.GetItem(idDto);
        return Ok(item);
    }

    /// <summary>
    /// Get a Order record for OrderItems
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<OrderDto>>> GetOrder([FromRoute()] OrderItemIdDto idDto)
    {
        var order = await _service.GetOrder(idDto);
        return Ok(order);
    }

    /// <summary>
    /// Meta data about OrderItems records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> OrderItemsMeta(
        [FromQuery()] OrderItemFindMany filter
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
        [FromRoute()] OrderItemIdDto idDto,
        [FromQuery()] OrderItemUpdateInput orderItemUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrderItem(idDto, orderItemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
