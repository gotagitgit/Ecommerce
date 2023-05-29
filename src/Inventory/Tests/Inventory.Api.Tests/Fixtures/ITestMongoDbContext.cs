using Inventory.Infrastructure.Persistance;

namespace Inventory.Api.Tests.Fixtures;

public interface ITestMongoDbContext : IMongoDbContext, IDisposable
{
}
