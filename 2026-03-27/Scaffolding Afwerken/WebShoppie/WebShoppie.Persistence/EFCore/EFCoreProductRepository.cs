using WebShoppie.DataModel;
using WebShoppie.Domain.Model;
using WebShoppie.Persistence.Exceptions;
using WebShoppie.Persistence.Interfaces;
using WebShoppie.Persistence.Mapping;

namespace WebShoppie.Persistence.EFCore;

public class EFCoreProductRepository(WebShoppieDbContext dbContext) : IProductRepository
{
    public ProductModel CreateProduct(ProductModel productModelToCreate)
    {
        var entity = productModelToCreate.AsEntity();
        dbContext.Products.Add(entity);
        dbContext.SaveChanges();
        return entity.AsModel();
    }

    public ProductModel? GetProductById(int id)
    {
        return dbContext.Products.Find(id)?.AsModel();
    }

    public List<ProductModel> GetProductsByIds(int[] ids)
    {
        var productEntities = dbContext.Products.Where(p => ids.ToList().Contains((int)p.Productid));
        return productEntities.Select(e => e.AsModel()).ToList();
    }

    public IEnumerable<ProductModel> GetAllProducts()
    {
        return dbContext.Products.Select(p => p.AsModel()).ToList();
    }

    public void UpdateProduct(ProductModel productModelToUpdate)
    {
        //style 1
        var entity = productModelToUpdate.AsEntity();
        dbContext.Products.Update(entity);
        
        //style 2 - fijnmazig
        /*
        var existing = dbContext.Products.Find(productModelToUpdate.ProductId);
        if (existing is null)
            throw new Exception("bestaat niet");
        existing.Description = productModelToUpdate.Description;
        existing.Stockcount = productModelToUpdate.StockCount;
        */
        
        dbContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var productToDelete = dbContext.Products.Find((long)id);

        if (productToDelete is null)
            throw new ProductEntityNotFoundException($"Product with id '{id}' not found!");
        
        dbContext.Products.Remove(productToDelete);
        dbContext.SaveChanges();
    }
}