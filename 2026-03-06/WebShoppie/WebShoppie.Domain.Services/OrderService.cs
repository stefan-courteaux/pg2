using WebShoppie.Api.Contracts.Orders;
using WebShoppie.Domain.Model;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Domain.Services.Mapping;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Domain.Services;

public class OrderService(IOrderRepository orderRepo, IProductRepository productRepo) : IOrderService
{
    //TODO is it ok to use 2 repo's in one service?
    public OrderResponseContract CreateOrder(OrderRequestContract orderToCreate)
    {
        var model = orderToCreate.AsModel();
        model.OrderDate = DateTime.Now;
        //TODO performance repeating query ok?
        model.OrderProducts = [];
        //TODO fix more elegant way
        model.OrderProducts.ForEach(op => op.Price = productRepo.GetProductById(op.ProductId).Price);
        var createdOrder = orderRepo.CreateOrder(model);
        return createdOrder.AsContract();
    }
}