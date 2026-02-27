using Microsoft.AspNetCore.Mvc;
using WebShoppie.Api.Contracts.Customers;
using WebShoppie.Domain.Services.Interfaces;

namespace WebShoppie.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpPost]
    public ActionResult<CustomerResponseContract> CreateCustomer([FromBody]CustomerRequestContract customerToCreate)
    {
        var created = customerService.CreateCustomer(customerToCreate);

        return CreatedAtAction(nameof(GetById), new { Id = created.CustomerId }, created);
    }
    
    [HttpGet("{id}")]
    public ActionResult<CustomerResponseContract> GetById([FromRoute]int id)
    {
        var customer = customerService.GetCustomerById(id);
        if (customer is null)
            return NotFound();
        return Ok(customer);
    }
}