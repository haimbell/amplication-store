using StoreService.APIs.Dtos;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Extensions;

public static class ItemsExtensions
{
    public static ItemDto ToDto(this Item model)
    {
        return new ItemDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            OrderItems = model.OrderItems?.Select(x => new OrderItemIdDto { Id = x.Id }).ToList(),
            Price = model.Price,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Item ToModel(this ItemUpdateInput updateDto, ItemIdDto idDto)
    {
        var item = new Item
        {
            Id = idDto.Id,
            Name = updateDto.Name,
            Price = updateDto.Price
        };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            item.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            item.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return item;
    }
}
