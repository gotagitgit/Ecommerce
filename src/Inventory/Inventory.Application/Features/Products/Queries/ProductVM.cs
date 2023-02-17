namespace Inventory.Application.Features.Products.Queries
{
    public sealed class ProductVM
    {
        public Guid SKU { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }
    }
}
