using FreshMarket.Domain.Interfaces.Repositories;
using FreshMarket.Domain.Interfaces.Services;
using FreshMarket.Infrastructure.Persistence;
using FreshMarket.Infrastructure.Persistence.Interceptors;
using FreshMarket.Infrastructure.Persistence.Repositories;
using FreshMarket.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FreshMarket.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICommonRepository, CommonRepository>();

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleItemService, SaleItemService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<ISupplyService, SupplyService>();
            services.AddScoped<ISupplyItemService, SupplyItemService>();

            return services;
        }

        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs/logs.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.File("logs/error_.txt", Serilog.Events.LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();

            return services;
        }

        public static IServiceCollection ConfigureDatabaseContext(this IServiceCollection services)
        {
            var builder = WebApplication.CreateBuilder();

            services.AddDbContext<FreshMarketDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SupermarketUzConnection")));

            return services;
        }
    }
}
