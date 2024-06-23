using StoreService.APIs.Dtos;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Extensions;

public static class OrdersExtensions
{
    public static OrderDto ToDto(this Order model)
    {
        return new OrderDto
        {
            CreatedAt = model.CreatedAt,
            Customer = new CustomerIdDto { Id = model.CustomerId },
            Id = model.Id,
            OrderItems = model.OrderItems?.Select(x => new OrderItemIdDto { Id = x.Id }).ToList(),
            Status = model.Status,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Order ToModel(this OrderUpdateInput updateDto, OrderIdDto idDto)
    {
        var order = new Order { Id = idDto.Id, Status = updateDto.Status };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            order.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            order.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return order;
    }
}
