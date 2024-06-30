using ServiceClean.APIs.Dtos;
using ServiceClean.Infrastructure.Models;

namespace ServiceClean.APIs.Extensions;

public static class CustomersExtensions
{
    public static CustomerDto ToDto(this Customer model)
    {
        return new CustomerDto
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static Customer ToModel(this CustomerUpdateInput updateDto, CustomerIdDto idDto)
    {
        var customer = new Customer { Id = idDto.Id };

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
