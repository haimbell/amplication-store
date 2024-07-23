using StoreService.APIs.Dtos;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Extensions;

public static class ItemsExtensions
{
    public static Item ToDto(this ItemDbModel model)
    {
        return new Item
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            Price = model.Price,
            OrderItems = model.OrderItems?.Select(x => x.Id).ToList(),
        };
    }

    public static ItemDbModel ToModel(this ItemUpdateInput updateDto, ItemWhereUniqueInput uniqueId)
    {
        var item = new ItemDbModel
        {
            Id = uniqueId.Id,
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
