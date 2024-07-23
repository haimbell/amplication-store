using StoreService.APIs;

namespace StoreService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddScoped<IOrderItemsService, OrderItemsService>();
    }
}
