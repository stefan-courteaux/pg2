using WebShoppie.DataModel.Entities;
using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Mapping;

public static class CustomerMappingExtensions
{
    public static Customer AsEntity(this CustomerModel model)
    {
        return new Customer
        {
            Customerid = model.CustomerId ?? 0,
            Firstname = model.FirstName,
            Lastname = model.LastName,
            Dateofbirth = model.DateOfBirth,
            Addressline1 = model.AddressLine1,
            Addressline2 = model.AddressLine2,
            Addressline3 = model.AddressLine3,
            Country = model.Country,
            Email = model.Email
        };
    }
    
    public static CustomerModel AsModel(this Customer entity)
    {
        return new CustomerModel
        {
            CustomerId = (int)entity.Customerid,
            FirstName = entity.Firstname,
            LastName = entity.Lastname,
            AddressLine1 = entity.Addressline1,
            AddressLine2 = entity.Addressline2,
            AddressLine3 = entity.Addressline3,
            Country = entity.Country,
            DateOfBirth = entity.Dateofbirth,
            Email = entity.Email
        };
    }
}