using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.SQLiteInfrastructure.Products.Dtos;

[Table("Products")]
public class ProductDto
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public int Quantity { get; set; }
}
