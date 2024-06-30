using ServiceClean.APIs.Common;
using ServiceClean.APIs.Dtos;

namespace ServiceClean.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<CustomerDto> CreateCustomer(CustomerCreateInput customerDto);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindMany findManyArgs);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerIdDto idDto);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<CustomerDto> Customer(CustomerIdDto idDto);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto);
}
