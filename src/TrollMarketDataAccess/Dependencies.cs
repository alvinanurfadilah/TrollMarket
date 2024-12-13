using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrollMarketDataAccess.Models;

namespace TrollMarketDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<TrollMarketContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("TrollMarketConnection"))
        );
    }
}
