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
    public async Task<ActionResult<ItemDto>> CreateItem(ItemCreateInput input)
    {
        var item = await _service.CreateItem(input);

        return CreatedAtAction(nameof(Item), new { id = item.Id }, item);
    }

    /// <summary>
    /// Delete one Item
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteItem([FromRoute()] ItemIdDto idDto)
    {
        try
        {
            await _service.DeleteItem(idDto);
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
    public async Task<ActionResult<List<ItemDto>>> Items([FromQuery()] ItemFindMany filter)
    {
        return Ok(await _service.Items(filter));
    }

    /// <summary>
    /// Get one Item
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<ItemDto>> Item([FromRoute()] ItemIdDto idDto)
    {
        try
        {
            return await _service.Item(idDto);
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
        [FromRoute()] ItemIdDto idDto,
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
    /// Disconnect multiple OrderItems records from Item
    /// </summary>
    [HttpDelete("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectOrderItems(
        [FromRoute()] ItemIdDto idDto,
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
    /// Find multiple OrderItems records for Item
    /// </summary>
    [HttpGet("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<OrderItemDto>>> FindOrderItems(
        [FromRoute()] ItemIdDto idDto,
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
    /// Meta data about Item records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ItemsMeta([FromQuery()] ItemFindMany filter)
    {
        return Ok(await _service.ItemsMeta(filter));
    }

    /// <summary>
    /// Update multiple OrderItems records for Item
    /// </summary>
    [HttpPatch("{Id}/orderItems")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateOrderItems(
        [FromRoute()] ItemIdDto idDto,
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
    /// Update one Item
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateItem(
        [FromRoute()] ItemIdDto idDto,
        [FromQuery()] ItemUpdateInput itemUpdateDto
    )
    {
        try
        {
            await _service.UpdateItem(idDto, itemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
