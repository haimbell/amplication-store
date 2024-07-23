using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreService.APIs;
using StoreService.APIs.Common;
using StoreService.APIs.Dtos;
using StoreService.APIs.Errors;

namespace StoreService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ItemsControllerBase : ControllerBase
{
    protected readonly IItemsService _service;

    public ItemsControllerBase(IItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Item
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Item>> CreateItem(ItemCreateInput input)
    {
        var item = await _service.CreateItem(input);

        return CreatedAtAction(nameof(Item), new { id = item.Id }, item);
    }

    /// <summary>
    /// Delete one Item
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteItem([FromRoute()] ItemWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteItem(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Items
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Item>>> Items([FromQuery()] ItemFindManyArgs filter)
    {
        return Ok(await _service.Items(filter));
    }

    /// <summary>
    /// Get one Item
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Item>> Item([FromRoute()] ItemWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Item(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple OrderItems records to Item
    /// </summary>
    [HttpPost("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectOrderItems(
        [FromRoute()] ItemWhereUniqueInput uniqueId,
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
    /// Disconnect multiple OrderItems records from Item
    /// </summary>
    [HttpDelete("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectOrderItems(
        [FromRoute()] ItemWhereUniqueInput uniqueId,
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
    /// Find multiple OrderItems records for Item
    /// </summary>
    [HttpGet("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<OrderItem>>> FindOrderItems(
        [FromRoute()] ItemWhereUniqueInput uniqueId,
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
    /// Meta data about Item records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ItemsMeta([FromQuery()] ItemFindManyArgs filter)
    {
        return Ok(await _service.ItemsMeta(filter));
    }

    /// <summary>
    /// Update multiple OrderItems records for Item
    /// </summary>
    [HttpPatch("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrderItems(
        [FromRoute()] ItemWhereUniqueInput uniqueId,
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
    /// Update one Item
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateItem(
        [FromRoute()] ItemWhereUniqueInput uniqueId,
        [FromQuery()] ItemUpdateInput itemUpdateDto
    )
    {
        try
        {
            await _service.UpdateItem(uniqueId, itemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
