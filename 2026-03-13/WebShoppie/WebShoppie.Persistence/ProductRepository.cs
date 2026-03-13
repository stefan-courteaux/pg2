using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence;

public class ProductRepository(IConfiguration configuration) : IProductRepository
{
    private readonly string connectionstring = configuration.GetConnectionString("Postgres");

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

    public List<Product> GetProductsByIds(int[] ids)
    {
        //TODO IN did not work with dapper + postgres. ANY does...
        //Why no ANSI support?
        const string query =
            @"SELECT * FROM storefront.Products WHERE ProductId = ANY(@Id)";

        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.Query<Product>(query, new { Id = ids.ToList() });

        return result.ToList();    
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