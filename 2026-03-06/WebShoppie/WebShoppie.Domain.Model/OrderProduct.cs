namespace WebShoppie.Domain.Model;

public class OrderProduct
{
    public int? OrderProductId { get; set; }
    public int? OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public Product? Product { get; set; }
}