using WebShoppie.Api.Contracts.Customers;

namespace WebShoppie.Domain.Services.Interfaces;

public interface ICustomerService
{
    CustomerResponseContract CreateCustomer(CustomerRequestContract customerToCreate);
    CustomerResponseContract? GetCustomerById(int id);
    List<CustomerResponseContract> GetAll();
    bool Update(int id, CustomerRequestContract customerToUpdate);
    void Delete(int id);
}