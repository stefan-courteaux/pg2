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
        //TODO would prefer CREATED response
        //TODO created order total price is 0 and orderlines are not created
        return Ok(service.CreateOrder(orderToCreate));
    }
}