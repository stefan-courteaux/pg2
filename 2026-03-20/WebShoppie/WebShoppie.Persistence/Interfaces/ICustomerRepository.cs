using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Interfaces;

public interface ICustomerRepository
{
    CustomerModel CreateCustomer(CustomerModel customerModelToCreate);
    CustomerModel? GetCustomerById(int id);
    List<CustomerModel> GetAllCustomers();
    void UpdateCustomer(CustomerModel customerModelToUpdate);
    void DeleteCustomer(int Id);
}