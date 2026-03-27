using WebShoppie.Api.Contracts.Products;
using WebShoppie.Domain.Services.Exceptions;
using WebShoppie.Domain.Services.Interfaces;
using WebShoppie.Domain.Services.Mapping;
using WebShoppie.Persistence.Exceptions;
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

    // Opzettelijk andere exception handling dan in CustomerService.
    // Gewoon ter demonstratie.
    public void DeleteProduct(int id)
    {
        try
        {
            productRepository.DeleteProduct(id);
        }
        // catch data layer exception
        catch (ProductEntityNotFoundException penfe)
        {
            // throw business layer exception
            throw new ProductDeleteBusinessException("Cannot delete product because it does not exist!");
        }
    }
}