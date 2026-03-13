using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence;

public class OrderRepository(IConfiguration configuration) : IOrderRepository
{
    private readonly string connectionstring = configuration.GetConnectionString("Postgres");

    public Order CreateOrder(Order orderToCreate)
    {
        //TODO add transaction error handling

        var orderSql = @"INSERT INTO storefront.Orders (
                           orderdate, customerid, totalprice)
                         VALUES (@Orderdate, @CustomerId, @TotalPrice)
                         RETURNING OrderId";

        using var connection = new NpgsqlConnection(connectionstring);

        connection.Open();
        using var tran = connection.BeginTransaction();
        var orderId = connection.ExecuteScalar<int>(orderSql, orderToCreate);
        orderToCreate.OrderProducts.ForEach(op => op.OrderId = orderId);

        var orderProductSql = @"INSERT INTO storefront.orderproducts (
                                      orderid, productid, quantity, price) 
                                VALUES (@OrderId, @ProductId, @Quantity, @Price)";

        connection.Execute(orderProductSql, orderToCreate.OrderProducts);
        tran.Commit();

        return GetOrder(orderId);
    }

    public Order? GetOrder(int id)
    {
        var sql = @"select o.orderid, o.orderdate, o.totalprice,
                   c.customerid, c.email, c.country,
                   op.orderproductid, op.quantity, op.price,
                   p.productid, p.title
        from storefront.orders o 
        inner join storefront.customers c on o.customerid = c.customerid
        inner join storefront.orderproducts op on op.orderid = o.orderid
        inner join storefront.products p on p.productid = op.productid
        where o.orderid = @Id";
        
        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.Query<Order, Customer, OrderProduct, Product, Order>(
            sql, 
            param: new { Id = id },
            map: (order, customer, orderProduct, product) =>
            {
                order.Customer = customer;
                order.CustomerId = customer.CustomerId!.Value;
                orderProduct.Product = product;
                orderProduct.ProductId = product.ProductId!.Value;
                order.OrderProducts = [orderProduct];
                return order;
            },
            splitOn: "customerid, orderproductid, productid"
            ).ToList();

        if (result.Count == 0)
            return null;

        var baseOrder = result.First();
        baseOrder.OrderProducts = result.SelectMany(o => o.OrderProducts!).ToList();

        return baseOrder;

    }
}