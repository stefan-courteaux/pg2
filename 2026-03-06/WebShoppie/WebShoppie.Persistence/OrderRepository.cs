using Dapper;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence;

public class OrderRepository : IOrderRepository
{
    //TODO dit proper uitlezen.
    private string connectionstring = "Server=localhost;Port=5432;Userid=admin;Password=secret;Database=pg2db";

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

        //TODO return something valuable
        return new Order(){OrderId = orderId};
    }

    public Order GetOrder(int id)
    {
        throw new NotImplementedException();
    }
}