using WebShoppie.Domain.Model;

namespace WebShoppie.Persistence.Interfaces;

public interface IProductRepository
{
    ProductModel CreateProduct(ProductModel productModelToCreate);
    ProductModel? GetProductById(int id);
    List<ProductModel> GetProductsByIds(int[] ids);
    IEnumerable<ProductModel> GetAllProducts();
    void UpdateProduct(ProductModel productModelToUpdate);
    void DeleteProduct(int id);
}