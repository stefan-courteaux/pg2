using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Exceptions;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence;

public class CustomerRepository(IConfiguration configuration) : ICustomerRepository
{
    private readonly string connectionstring = configuration.GetConnectionString("Postgres");
    
    public Customer CreateCustomer(Customer customerToCreate)
    {
        const string query =
            @"INSERT INTO storefront.customers(
                firstname, lastname, dateofbirth, email,
                addressline1, addressline2, addressline3, country)
                      VALUES (@firstname, @lastname, @dateofbirth, @email,
                @addressline1, @addressline2, @addressline3, @country)
                      RETURNING CustomerId";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        var id = connection.ExecuteScalar<int>(query, customerToCreate);
        
        return GetCustomerById(id) ?? throw new Exception(
            $"Cust {customerToCreate.FirstName} could not be created"
        );        
    }
    
    public Customer? GetCustomerById(int id)
    {
        const string query =
            @"SELECT * FROM Storefront.Customers WHERE CustomerId = @Id";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        var result = connection.QuerySingleOrDefault<Customer>(query, new {Id = id});

        return result;
    }

    public List<Customer> GetAllCustomers()
    {
        const string query =
            @"SELECT * FROM Storefront.Customers";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        var result = connection.Query<Customer>(query);

        return result.ToList();
    }

    public void UpdateCustomer(Customer customerToUpdate)
    {
        const string query =
            @"UPDATE storefront.Customers
               SET firstname = @FirstName,
                   lastname = @LastName,
                   dateofbirth = @DateOfBirth
            WHERE CustomerId = @CustomerId"; // etc
        
        using var connection = new NpgsqlConnection(connectionstring);

        var rowsAffected = connection.Execute(query, customerToUpdate);
        if (rowsAffected == 0)
            throw new OmgCustomerDoesNotExistInDbException(customerToUpdate.CustomerId.ToString());
    }

    public void DeleteCustomer(int Id)
    {
        const string query =
            @"DELETE FROM Storefront.Customers WHERE CustomerId = @Id";
        
        using var connection = new NpgsqlConnection(connectionstring);
        
        connection.Execute(query, new {Id = Id});

    }
}