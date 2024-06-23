using StoreService.APIs.Dtos;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Extensions;

public static class OrderItemsExtensions
{
    public static OrderItemDto ToDto(this OrderItem model)
    {
        return new OrderItemDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Item = new ItemIdDto { Id = model.ItemId },
            Order = new OrderIdDto { Id = model.OrderId },
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static OrderItem ToModel(this OrderItemUpdateInput updateDto, OrderItemIdDto idDto)
    {
        var orderItem = new OrderItem { Id = idDto.Id };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            orderItem.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            orderItem.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return orderItem;
    }
}
