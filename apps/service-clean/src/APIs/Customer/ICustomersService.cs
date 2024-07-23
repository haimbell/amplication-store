using ServiceClean.APIs.Common;
using ServiceClean.APIs.Dtos;

namespace ServiceClean.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);
}
