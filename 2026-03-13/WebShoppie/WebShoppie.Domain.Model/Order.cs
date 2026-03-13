namespace WebShoppie.Domain.Model;

public class Order
{
    public int? OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal? TotalPrice => OrderProducts?.Sum(op => op.Price);
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public List<OrderProduct>? OrderProducts { get; set; }
}