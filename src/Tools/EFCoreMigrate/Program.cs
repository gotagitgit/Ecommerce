using Inventory.SQLiteInfrastructure;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Persistance.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreMigrate;

internal class Program
{
    static void Main(string[] args)
    {
        var dbContextFactory = new DbContextFactory();

        using var dbContext = dbContextFactory.CreateDbContext(args);

        OverwriteDatabaseBeforeMigrate(dbContext);

        var serviceCollection = new ServiceCollection();

        ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        CreateDatabaseSeedData(dbContext, serviceProvider);

    }

    private static void CreateDatabaseSeedData(SQLiteDbContext dbContext, ServiceProvider serviceProvider)
    {
        var dBContextSeeder = serviceProvider.GetService<IDBContextSeeder>();

        dBContextSeeder?.Seed(dbContext);
    }

    private static void OverwriteDatabaseBeforeMigrate(SQLiteDbContext dbContext)
    {
        dbContext.Database.EnsureDeleted();

        dbContext.Database.EnsureCreated();

        dbContext.Database.Migrate();
    }

    private static void ConfigureServices(ServiceCollection serviceCollection)
    {
        serviceCollection.RegisterSQLiteInfrastureDependencies();
    }
}
