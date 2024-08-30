using Infrastructure.Common;
using Inventory.Application.Repositories;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Persistance.Seeders;
using Inventory.SQLiteInfrastructure.Products.Repositories;
using Inventory.SQLiteInfrastructure.Quotes.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.SQLiteInfrastructure;

public static class SQLiteDependencyInjection
{
    public static IServiceCollection RegisterSQLiteInfrastureDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IQuoteRepository, QuoteRepository>();

        services.AddScoped<IDatabaseContext>(x => x.GetRequiredService<SQLiteDbContext>());
        services.AddDbContext<SQLiteDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDBContextSeeder, ProductsSeeder>();

        return services;
    }
}
