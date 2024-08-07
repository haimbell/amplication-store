using Microsoft.AspNetCore.Mvc;
using ServiceClean.APIs.Common;
using ServiceClean.Infrastructure.Models;

namespace ServiceClean.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
