using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TrollMarketAPI.Accounts;
using TrollMarketAPI.Carts;
using TrollMarketAPI.Merchandise;
using TrollMarketAPI.Shipments;
using TrollMarketBusiness.Interfaces;
using TrollMarketBusiness.Repositories;
using static TrollMarketDataAccess.Dependencies;

namespace TrollMarketAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;
        ConfigureService(configuration, services);
        services.AddControllers();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<AccountService>();
        services.AddScoped<IShipmentRepository, ShipmentRepository>();
        services.AddScoped<ShipmentService>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<CartService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<MerchandiseService>();

        builder.Services.AddCors(option => {
            option.AddPolicy(name: "AllowFrontEnd", builder => {
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value)
                ),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        services.AddSwaggerGen(options => {
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "Troll Market API"
            });
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Description = "Enter the token with the `Bearer: ` prefix, e.g. 'Bearer fdhauy837r3'",
                In = ParameterLocation.Header,
                Name = "Account",
                Type = SecuritySchemeType.ApiKey
            });
            // options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        var app = builder.Build();
        app.UseCors("AllowFrontEnd");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
