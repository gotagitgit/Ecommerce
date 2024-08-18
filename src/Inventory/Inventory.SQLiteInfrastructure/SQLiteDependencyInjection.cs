using Infrastructure.Common;
using Inventory.Application.Repositories;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Persistance.Seeders;
using Inventory.SQLiteInfrastructure.Products.Repositories;
using Inventory.SQLiteInfrastructure.UOW;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.SQLiteInfrastructure;

public static class SQLiteDependencyInjection
{
    public static IServiceCollection RegisterSQLiteInfrastureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork<SQLiteDbContext>, UnitOfWork>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDatabaseContext<SQLiteDbContext>, SQLiteDbContext>();

        //services.AddDbContext<SQLiteDbContext>(options =>
        //            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDBContextSeeder, ProductsSeeder>();

        return services;
    }
}
