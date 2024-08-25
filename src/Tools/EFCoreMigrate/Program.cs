using Inventory.SQLiteInfrastructure;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Persistance.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreMigrate;

internal class Program
{
    static void Main(string[] args)
    {
        var dbContextFactory = new DbContextFactory();

        using var dbContext = dbContextFactory.CreateDbContext(args);        
                
        //dbContext.Database.EnsureDeleted();

        dbContext.Database.EnsureCreated();   

        dbContext.Database.Migrate();

        var serviceCollection = new ServiceCollection();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        ConfigureServices(serviceCollection, serviceProvider);

        var dBContextSeeder = serviceProvider.GetService<IDBContextSeeder>();

        dBContextSeeder?.Seed(dbContext);

    }

    //private static void CreateDatabaseSeedData(SQLiteDbContext dbContext, ServiceProvider serviceProvider)
    //{
    //    var dBContextSeeder = serviceProvider.GetService<IDBContextSeeder>();

    //    dBContextSeeder?.Seed(dbContext);
    //}

    //private static void OverwriteDatabaseBeforeMigrate(SQLiteDbContext dbContext)
    //{
    //    dbContext.Database.EnsureDeleted();

    //    //dbContext.Database.EnsureCreated();

    //    dbContext.Database.Migrate();
    //}

    private static void ConfigureServices(ServiceCollection serviceCollection, ServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetService<IConfiguration>();

        serviceCollection.RegisterSQLiteInfrastureDependencies(configuration);
    }
}
