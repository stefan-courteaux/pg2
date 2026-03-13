namespace WebShoppie.Domain.Model;

public class Product
{
    public int? ProductId { get; set; }

    public required string Title { get; set; }

    public required string? Description { get; set; }

    public required decimal Price { get; set; }

    public required int StockCount { get; set; }
}