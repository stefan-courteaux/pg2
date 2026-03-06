using Dapper;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence;

public class ProductRepository : IProductRepository
{
    //TODO dit proper uitlezen.
    private string connectionstring = "Server=localhost;Port=5432;Userid=admin;Password=secret;Database=pg2db";

    public Product CreateProduct(Product productToCreate)
    {
        const string query =
            @"INSERT INTO storefront.products(
                title, description, price, stockcount)
                      VALUES (@title, @description, @price, @stockcount)
                      RETURNING ProductId";

        using var connection = new NpgsqlConnection(connectionstring);

        var id = connection.ExecuteScalar<int>(query, productToCreate);

        return GetProductById(id) ?? throw new Exception(
            $"Product {productToCreate.Title} could not be created"
        );
    }

    public Product? GetProductById(int id)
    {
        const string query =
            @"SELECT * FROM storefront.Products WHERE ProductId = @Id";

        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.QuerySingleOrDefault<Product>(query, new { Id = id });

        return result;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        const string query =
            @"SELECT * FROM storefront.Products";

        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.Query<Product>(query);

        return result;
    }

    public void UpdateProduct(Product productToUpdate)
    {
        const string query =
            @"UPDATE storefront.Products 
            SET title = @title, 
                description = @description, 
                price = @price, 
                stockcount = @stockcount      
            WHERE ProductId = @ProductId";

        using var connection = new NpgsqlConnection(connectionstring);

        var affected = connection.Execute(query, productToUpdate);
    }

    public void DeleteProduct(int id)
    {
        const string query =
            @"DELETE FROM storefront.Products 
            WHERE ProductId = @ProductId";

        using var connection = new NpgsqlConnection(connectionstring);

        var affected = connection.Execute(query, new { ProductId = id });    
    }
}