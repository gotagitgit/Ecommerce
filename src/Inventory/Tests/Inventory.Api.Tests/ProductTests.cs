using FluentAssertions;
using Inventory.Api.Tests.Fixtures;
using Inventory.Api.Tests.Services;
using Inventory.Application.Features.Products.Commands.AddProduct;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Inventory.Api.Tests;

public class ProductTests : IAsyncLifetime
{
    private readonly TestContext _context;
    private readonly IInventoryClientService _inventoryClient;

    public ProductTests(ITestOutputHelper testOutputHelper)
    {
        _context = new TestContext(testOutputHelper);

        _inventoryClient = _context.InventoryClient;
    }    

    public Task DisposeAsync()
    {
        _context.Dispose();

        return Task.CompletedTask;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task Should_create_pproducts()
    {
        // Arrange
        var addProduct = new AddProductCommand { Name = "Test" };

        // Act
        var result = await _inventoryClient.PostAsync(addProduct);

        // Assert
        result.Should().NotBe(Guid.Empty);
    }

    public sealed class TestContext : IDisposable
    {
        private readonly InventoryFixture _inventoryFixture;

        public TestContext(ITestOutputHelper testOutputHelper)
        {
            _inventoryFixture = new InventoryFixture(testOutputHelper);

            InventoryClient = _inventoryFixture.InventoryClient;
        }

        public IInventoryClientService InventoryClient { get; }

        public void Dispose()
        {
            _inventoryFixture.Dispose();
        }
    }
}