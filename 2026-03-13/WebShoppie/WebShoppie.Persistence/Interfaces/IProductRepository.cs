using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Interfaces;

public interface IProductRepository
{
    Product CreateProduct(Product productToCreate);
    Product? GetProductById(int id);
    List<Product> GetProductsByIds(int[] ids);
    IEnumerable<Product> GetAllProducts();
    void UpdateProduct(Product productToUpdate);
    void DeleteProduct(int id);
}