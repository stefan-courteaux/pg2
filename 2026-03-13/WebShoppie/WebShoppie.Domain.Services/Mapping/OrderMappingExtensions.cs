using WebShoppie.Api.Contracts.Orders;
using WebShoppie.Domain.Model;

namespace WebShoppie.Domain.Services.Mapping;

public static class OrderMappingExtensions
{
    public static Order AsModel(this OrderRequestContract contract)
    {
        return new Order()
        {
            CustomerId = contract.CustomerId,
            OrderProducts = contract.OrderProducts.Select(op => new OrderProduct()
            {
                ProductId = op.ProductId,
                Quantity = op.Quantity
            }).ToList()
        };
    }

    public static OrderResponseContract? AsContract(this Order? model)
    {
        if(model is null)
            return null;

        if (model.Customer?.CustomerId is null || model.OrderId is null || model.OrderProducts is null)
            throw new IllegalMappingException();
        
        return new OrderResponseContract()
        {
            OrderId = model.OrderId.Value,
            CustomerId = model.Customer.CustomerId.Value,
            Email = model.Customer.Email,
            OrderDate = model.OrderDate,
            OrderItems = model.OrderProducts.Select(op => new OrderResponseProductContract()
            {
                ProductId = op.Product.ProductId.Value,
                Quantity = op.Quantity,
                Title = op.Product.Title,
                UnitPriceAtPurchase = op.Price
            }).ToList()
        };
    }
}

public class IllegalMappingException : Exception
{
}