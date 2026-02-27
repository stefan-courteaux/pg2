using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Interfaces;

public interface ICustomerRepository
{
    Customer CreateCustomer(Customer customerToCreate);
    Customer? GetCustomerById(int id);

}