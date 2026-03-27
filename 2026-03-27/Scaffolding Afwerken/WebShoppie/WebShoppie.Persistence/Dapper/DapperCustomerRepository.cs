using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Exceptions;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence.Dapper;

public class DapperCustomerRepository(IConfiguration configuration) : ICustomerRepository
{
    private readonly string connectionstring = configuration.GetConnectionString("Postgres");
    
    public CustomerModel CreateCustomer(CustomerModel customerModelToCreate)
    {
        const string query =
            @"INSERT INTO storefront.customers(
                firstname, lastname, dateofbirth, email,
                addressline1, addressline2, addressline3, country)
                      VALUES (@firstname, @lastname, @dateofbirth, @email,
                @addressline1, @addressline2, @addressline3, @country)
                      RETURNING CustomerId";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        var id = connection.ExecuteScalar<int>(query, customerModelToCreate);
        
        return GetCustomerById(id) ?? throw new Exception(
            $"Cust {customerModelToCreate.FirstName} could not be created"
        );        
    }
    
    public CustomerModel? GetCustomerById(int id)
    {
        const string query =
            @"SELECT * FROM Storefront.Customers WHERE CustomerId = @Id";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        var result = connection.QuerySingleOrDefault<CustomerModel>(query, new {Id = id});

        return result;
    }

    public List<CustomerModel> GetAllCustomers()
    {
        const string query =
            @"SELECT * FROM Storefront.Customers";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        var result = connection.Query<CustomerModel>(query);

        return result.ToList();
    }

    public void UpdateCustomer(CustomerModel customerModelToUpdate)
    {
        const string query =
            @"UPDATE storefront.Customers
               SET firstname = @FirstName,
                   lastname = @LastName,
                   dateofbirth = @DateOfBirth
            WHERE CustomerId = @CustomerId"; // etc
        
        using var connection = new NpgsqlConnection(connectionstring);

        var rowsAffected = connection.Execute(query, customerModelToUpdate);
        if (rowsAffected == 0)
            throw new OmgCustomerDoesNotExistInDbException(customerModelToUpdate.CustomerId.ToString());
    }

    public void DeleteCustomer(int Id)
    {
        const string query =
            @"DELETE FROM Storefront.Customers WHERE CustomerId = @Id";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        connection.Execute(query, new {Id = Id});

    }
}