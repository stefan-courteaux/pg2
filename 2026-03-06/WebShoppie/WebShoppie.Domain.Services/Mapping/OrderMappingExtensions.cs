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

    public static OrderResponseContract? AsContract(this Order model)
    {
        //TODO expand order response to include full fields and hierarchy
        return new OrderResponseContract()
        {
            OrderId = model.OrderId.Value
        };
    }
}