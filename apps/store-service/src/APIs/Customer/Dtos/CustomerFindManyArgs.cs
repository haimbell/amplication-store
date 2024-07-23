using Microsoft.AspNetCore.Mvc;
using StoreService.APIs.Common;
using StoreService.Infrastructure.Models;

namespace StoreService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class CustomerFindManyArgs : FindManyInput<Customer, CustomerWhereInput> { }
