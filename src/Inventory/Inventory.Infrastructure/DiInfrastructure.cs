using Inventory.Application.Repositories;
using Inventory.Infrastructure.Factories;
using Inventory.Infrastructure.Persistance;
using Inventory.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure;

public static class DiInfrastructure
{
    public static IServiceCollection RegisterInfrastureDependencies(this IServiceCollection services)
    {
        services.AddScoped<IMongoDbContext, MongoDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
