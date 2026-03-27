using WebShoppie.Api.Contracts.Products;

namespace WebShoppie.Domain.Services.Interfaces;

public interface IProductService
{
    ProductResponseContract CreateProduct(ProductRequestContract ProductToCreate);
    ProductResponseContract? GetProductById(int id);
    IEnumerable<ProductResponseContract> GetAllProducts();
    void UpdateProduct(int productId, ProductRequestContract ProductToUpdate);
    void DeleteProduct(int id);
}