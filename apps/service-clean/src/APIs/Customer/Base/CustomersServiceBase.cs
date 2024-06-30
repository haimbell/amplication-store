using Microsoft.EntityFrameworkCore;
using ServiceClean.APIs;
using ServiceClean.APIs.Common;
using ServiceClean.APIs.Dtos;
using ServiceClean.APIs.Errors;
using ServiceClean.APIs.Extensions;
using ServiceClean.Infrastructure;
using ServiceClean.Infrastructure.Models;

namespace ServiceClean.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly ServiceCleanDbContext _context;

    public CustomersServiceBase(ServiceCleanDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<CustomerDto> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new Customer
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Customer>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public async Task<MetadataDto> CustomersMeta(CustomerFindMany findManyArgs)
    {
        var count = await _context.Customers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerIdDto idDto)
    {
        var customer = await _context.Customers.FindAsync(idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs)
    {
        var customers = await _context
            .Customers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<CustomerDto> Customer(CustomerIdDto idDto)
    {
        var customers = await this.Customers(
            new CustomerFindMany { Where = new CustomerWhereInput { Id = idDto.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto)
    {
        var customer = updateDto.ToModel(idDto);

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
