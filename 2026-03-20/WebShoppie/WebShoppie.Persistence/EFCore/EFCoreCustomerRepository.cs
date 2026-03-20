using WebShoppie.DataModel;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;
using WebShoppie.Persistence.Mapping;

namespace WebShoppie.Persistence.EFCore;

public class EFCoreCustomerRepository(WebShoppieDbContext dbContext) : ICustomerRepository
{
    public CustomerModel CreateCustomer(CustomerModel customerModelToCreate)
    {
        
        var entity = customerModelToCreate.AsEntity();
        dbContext.Customers.Add(entity);
        dbContext.SaveChanges();
        return entity.AsModel();
    }

    public CustomerModel? GetCustomerById(int id)
    {
        return dbContext.Customers.Find((long)id).AsModel();
    }

    public List<CustomerModel> GetAllCustomers()
    {
        return dbContext.Customers.Select(c => c.AsModel()).ToList();
    }

    public void UpdateCustomer(CustomerModel customerModelToUpdate)
    {
        var toUpdate = customerModelToUpdate.AsEntity();
        dbContext.Customers.Update(toUpdate);
        dbContext.SaveChanges();
    }

    public void DeleteCustomer(int Id)
    {
        var entity = dbContext.Customers.Find((long)Id);
        dbContext.Customers.Remove(entity);
        dbContext.SaveChanges();
    }
}