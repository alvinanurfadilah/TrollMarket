using TrollMarketBusiness.Interfaces;
using TrollMarketBusiness.Repositories;
using TrollMarketWeb.Services;

namespace TrollMarketWeb;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<AccountSerice>();
        services.AddScoped<ProfileService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ShopService>();
        services.AddScoped<MerchandiseService>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<ShipmentService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<CartService>();
        services.AddScoped<AdminService>();
        services.AddScoped<HistoryService>();
        return services;
    }
}
