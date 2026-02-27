using WebShoppie.Api.Contracts.Customers;

namespace WebShoppie.Domain.Services.Interfaces;

public interface ICustomerService
{
    CustomerResponseContract CreateCustomer(CustomerRequestContract customerToCreate);
    CustomerResponseContract? GetCustomerById(int id);
}