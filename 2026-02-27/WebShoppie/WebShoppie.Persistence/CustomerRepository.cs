using Dapper;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence;

public class CustomerRepository : ICustomerRepository
{
    //TODO dit proper uitlezen.
    private string connectionstring = "Server=localhost;Port=5432;Userid=admin;Password=secret;Database=pg2db";
    
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
}