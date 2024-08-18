using Inventory.SQLiteInfrastructure.Persistance;

namespace Inventory.SQLiteInfrastructure.Persistance.Seeders;

public interface IDBContextSeeder
{
    void Seed(SQLiteDbContext context);
}