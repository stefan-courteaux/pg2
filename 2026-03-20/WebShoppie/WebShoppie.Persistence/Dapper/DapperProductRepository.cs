using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Persistence.Dapper;

public class DapperProductRepository(IConfiguration configuration) : IProductRepository
{
    private readonly string connectionstring = configuration.GetConnectionString("Postgres");

    public ProductModel CreateProduct(ProductModel productModelToCreate)
    {
        const string query =
            @"INSERT INTO storefront.products(
                title, description, price, stockcount)
                      VALUES (@title, @description, @price, @stockcount)
                      RETURNING ProductId";

        using var connection = new NpgsqlConnection(connectionstring);

        var id = connection.ExecuteScalar<int>(query, productModelToCreate);

        return GetProductById(id) ?? throw new Exception(
            $"Product {productModelToCreate.Title} could not be created"
        );
    }

    public ProductModel? GetProductById(int id)
    {
        const string query =
            @"SELECT * FROM storefront.Products WHERE ProductId = @Id";

        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.QuerySingleOrDefault<ProductModel>(query, new { Id = id });

        return result;
    }

    public List<ProductModel> GetProductsByIds(int[] ids)
    {
        //TODO IN did not work with dapper + postgres. ANY does...
        //Why no ANSI support?
        const string query =
            @"SELECT * FROM storefront.Products WHERE ProductId = ANY(@Id)";

        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.Query<ProductModel>(query, new { Id = ids.ToList() });

        return result.ToList();    
    }

    public IEnumerable<ProductModel> GetAllProducts()
    {
        const string query =
            @"SELECT * FROM storefront.Products";

        using var connection = new NpgsqlConnection(connectionstring);

        var result = connection.Query<ProductModel>(query);

        return result;
    }

    public void UpdateProduct(ProductModel productModelToUpdate)
    {
        const string query =
            @"UPDATE storefront.Products 
            SET title = @title, 
                description = @description, 
                price = @price, 
                stockcount = @stockcount      
            WHERE ProductId = @ProductId";

        using var connection = new NpgsqlConnection(connectionstring);

        var affected = connection.Execute(query, productModelToUpdate);
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