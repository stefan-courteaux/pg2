using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Interfaces;

public interface IOrderRepository
{
    OrderModel CreateOrder(OrderModel orderModelToCreate);
    OrderModel GetOrder(int id);
}