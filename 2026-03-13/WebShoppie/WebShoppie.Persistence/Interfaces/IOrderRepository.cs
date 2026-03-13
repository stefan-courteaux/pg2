using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Interfaces;

public interface IOrderRepository
{
    Order CreateOrder(Order orderToCreate);
    Order GetOrder(int id);
}