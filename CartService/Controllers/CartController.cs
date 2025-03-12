using Microsoft.AspNetCore.Mvc;
using CartService.Models;
using CartService.Repositories;

namespace CartService.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController(CartRepository _cartRepository) : ControllerBase
{
	// GET: api/cart/{userId}
	[HttpGet("{userId}")]
	public async Task<ActionResult<Cart>> GetCart(string userId)
	{
		var cart = await _cartRepository.GetCartAsync(userId);
		return cart ?? new Cart { UserId = userId };
	}

    // POST: api/cart/{userId}
	[HttpPost("{userId}")]
	public async Task<IActionResult> AddToCart(string userId, [FromBody] CartItem item)
	{
		var cart = await _cartRepository.GetCartAsync(userId)
			?? new Cart { UserId = userId };
		var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
		if(existingItem != null)
		{
			existingItem.Quantity += item.Quantity;
		}
		else
		{
			cart.Items.Add(item);
		}
		await _cartRepository.SaveCartAsync(cart);
		return Ok();
	}

	// DELETE: api/cart/{userId}
	[HttpDelete("{userId}")]
	public async Task<IActionResult> ClearCart(string userId)
	{
		await _cartRepository.DeleteCartAsync(userId);
		return NoContent();
	}
}