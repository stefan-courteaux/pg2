using WebShoppie.Api.Contracts.Products;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Domain.Services.Mapping;
using WebShoppie.Persistence.Interfaces;

namespace WebShoppie.Domain.Services;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public ProductResponseContract CreateProduct(ProductRequestContract productToCreate)
    {
        var model = productToCreate.AsModel();
        var created = productRepository.CreateProduct(model);
        var contract = created.AsContract();
        return contract;
    }

    public ProductResponseContract? GetProductById(int id)
    {
        return productRepository.GetProductById(id)?.AsContract();
    }

    public IEnumerable<ProductResponseContract> GetAllProducts()
    {
        return productRepository.GetAllProducts().Select(c => c.AsContract());
    }

    public void UpdateProduct(int productId, ProductRequestContract productToUpdate)
    {
        var model = productToUpdate.AsModel(productId);
        productRepository.UpdateProduct(model);
    }

    public void DeleteProduct(int id)
    {
        productRepository.DeleteProduct(id);
    }
}