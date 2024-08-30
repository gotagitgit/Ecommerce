using Inventory.SQLiteInfrastructure;
using Inventory.SQLiteInfrastructure.Persistance;
using Inventory.SQLiteInfrastructure.Persistance.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreMigrate;

internal class Program
{
    static async Task Main(string[] args)
    {
        var dbContextFactory = new DbContextFactory();

        using var dbContext = dbContextFactory.CreateDbContext(args);

        //await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Database.EnsureCreatedAsync();

        await dbContext.Database.MigrateAsync();
        

        var serviceCollection = new ServiceCollection();       

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var xxx = ConfigureServices(serviceCollection, serviceProvider);

        var dBContextSeeder = xxx.BuildServiceProvider().GetRequiredService<IDBContextSeeder>();

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

    private static IServiceCollection ConfigureServices(IServiceCollection serviceCollection, ServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetService<IConfiguration>();

        return serviceCollection.RegisterSQLiteInfrastureDependencies(configuration);
    }
}
