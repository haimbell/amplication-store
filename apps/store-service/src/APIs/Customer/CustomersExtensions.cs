using StoreService.APIs.Dtos;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Extensions;

public static class CustomersExtensions
{
    public static CustomerDto ToDto(this Customer model)
    {
        return new CustomerDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Orders = model.Orders?.Select(x => new OrderIdDto { Id = x.Id }).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Customer ToModel(this CustomerUpdateInput updateDto, CustomerIdDto idDto)
    {
        var customer = new Customer { Id = idDto.Id, Name = updateDto.Name };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            customer.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            customer.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return customer;
    }
}
