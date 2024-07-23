using StoreService.APIs.Dtos;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Extensions;

public static class OrderItemsExtensions
{
    public static OrderItem ToDto(this OrderItemDbModel model)
    {
        return new OrderItem
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Order = model.OrderId,
            Item = model.ItemId,
        };
    }

    public static OrderItemDbModel ToModel(
        this OrderItemUpdateInput updateDto,
        OrderItemWhereUniqueInput uniqueId
    )
    {
        var orderItem = new OrderItemDbModel { Id = uniqueId.Id };

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
