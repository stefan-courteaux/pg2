using Microsoft.AspNetCore.Mvc;
using WebShoppie.Api.Contracts.Orders;
using WebShoppie.Domain.Services.Interfaces;

namespace WebShoppie.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(IOrderService service) : ControllerBase
{
    [HttpPost]
    public ActionResult<OrderResponseContract> CreateOrder([FromBody] OrderRequestContract orderToCreate)
    {
        var created = service.CreateOrder(orderToCreate);
        return CreatedAtAction(nameof(GetOrder), new {Id = created.OrderId }, created);
        
    }

    [HttpGet("{id}")]
    public ActionResult<OrderResponseContract> GetOrder([FromRoute] int id)
    {
        var result = service.GetOrderById(id);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}