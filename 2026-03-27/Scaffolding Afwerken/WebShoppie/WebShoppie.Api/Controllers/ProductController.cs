using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebShoppie.Api.Contracts.Products;
using WebShoppie.Domain.Services.Exceptions;
using WebShoppie.Domain.Services.Interfaces;

namespace WebShoppie.Api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpPost]
    public ActionResult<ProductResponseContract> CreateProduct([FromBody] ProductRequestContract productToCreate)
    {
        var created = productService.CreateProduct(productToCreate);

        return Ok(created);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ProductResponseContract> GetById([FromRoute]int id)
    {
        var product = productService.GetProductById(id);
        if (product is null)
            return NotFound();
        return Ok(product);
    }

    [HttpGet]
    public ActionResult<List<ProductResponseContract>> GetAll()
    {
        return Ok(productService.GetAllProducts());
    }

    [HttpPut("{id}")]
    public ActionResult Update([FromRoute] int id, [FromBody] ProductRequestContract contract)
    {
        productService.UpdateProduct(id, contract);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        try
        {
            productService.DeleteProduct(id);
            return NoContent();
        }
        catch (ProductDeleteBusinessException e)
        {
            //return NotFound(e.Message);
            return Problem(title: "Product Delete Problem", detail: e.Message,
                statusCode: (int)HttpStatusCode.NotFound);
        }
    }        
}