using WebShoppie.Api.Contracts.Customers;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Domain.Services.Mapping;
using WebShoppie.Persistence;
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

    public List<CustomerResponseContract> GetAll()
    {
        return customerRepository.GetAllCustomers()
            .Select(c => c.AsContract())
            .ToList();
    }

    public bool Update(int id, CustomerRequestContract customerToUpdate)
    {
        var model = customerToUpdate.AsModel(id);
        try
        {
            customerRepository.UpdateCustomer(model);
            return true;
        }
        catch (OmgCustomerDoesNotExistInDBException e)
        {
            return false;
        }
    }

    public void Delete(int id)
    {
        customerRepository.DeleteCustomer(id);
    }
}