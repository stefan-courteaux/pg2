using WebShoppie.Api.Contracts.Customers;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Domain.Services.Mapping;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Domain.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    public CustomerResponseContract CreateCustomer(CustomerRequestContract customerToCreate)
    {
        var model = customerToCreate.AsModel();
        var created = customerRepository.CreateCustomer(model);
        var contract = created.AsContract();
        return contract;
    }

    public CustomerResponseContract? GetCustomerById(int id)
    {
        return customerRepository.GetCustomerById(id)?.AsContract();
    }
}