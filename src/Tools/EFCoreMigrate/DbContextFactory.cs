using Inventory.SQLiteInfrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace EFCoreMigrate;

public class DbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
{
    public SQLiteDbContext CreateDbContext(string[] args)
    {
        string sqliteDatabaseFileName = CreateSqliteDatabaseFileName();

        var connectionString = $"Data Source={sqliteDatabaseFileName}";

        var builder = new DbContextOptionsBuilder<SQLiteDbContext>();

        builder.UseSqlite(connectionString, x => x.MigrationsAssembly("Inventory.SQLiteInfrastructure")
                                                  .MigrationsHistoryTable("AppDbVersion"));

        return new SQLiteDbContext(builder.Options);
    }

    private static string CreateSqliteDatabaseFileName()
    {
        var executingAssembly = Assembly.GetExecutingAssembly().Location;

        var sqliteDatabaseRelativePath = Path.GetRelativePath(executingAssembly, "../../../../Inventory/Inventory.SQLiteInfrastructure/SQLiteDatabase");

        var sqliteDatabaseFullPath = Path.GetFullPath(sqliteDatabaseRelativePath);

        return Path.Combine(sqliteDatabaseFullPath, "Inventories.db");
    }
}
