using WebShoppie.Api.Contracts.Orders;

namespace WebShoppie.Domain.Services.Interfaces;

public interface IOrderService
{
    OrderResponseContract CreateOrder(OrderRequestContract orderToCreate);
}