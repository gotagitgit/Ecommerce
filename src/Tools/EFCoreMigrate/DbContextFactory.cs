using Inventory.SQLiteInfrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFCoreMigrate;

public class DbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
{
    public SQLiteDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Data Source=localhost\\MSSQLLOCALDB;Initial Catalog=Inventories;Integrated Security=True;Encrypt=false;TrustServerCertificate=true;";

        var builder = new DbContextOptionsBuilder<SQLiteDbContext>();

        builder.UseSqlServer(connectionString, x => x.MigrationsAssembly("Inventory.SQLiteInfrastructure")
                                                  .MigrationsHistoryTable("AppDbVersion"));

        return new SQLiteDbContext(builder.Options);
    }
}
