using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderService.Services;

namespace OrderService.Controllers;

[Route("orders")]
[ApiController]
public class OrderController(OrderServiceClass _orderService) : ControllerBase
{

	[HttpPost("{userId}")]
	public async Task<IActionResult> PlaceOrder(string userId)
	{
		try
		{
			var order = await _orderService.CreateOrder(userId);
			return Ok(order);
		}
		catch (System.Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}

