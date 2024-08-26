namespace Inventory.SQLiteInfrastructure.Dtos;

public class QuoteDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public DateTime DateCreated { get; set; }
}