using Microsoft.AspNetCore.Mvc;

using CartService.Models;
using CartService.Repositories;
using CartService.Services;

namespace CartService.Controllers;

[ApiController]
[Route("cart")]
public class CartController(CartRepository _cartRepository, ProductServiceClient _productService) : ControllerBase
{
	// GET: api/cart/{userId}
	[HttpGet("{userId}")]
	public async Task<ActionResult<Cart>> GetCart(string userId)
	{
		var cart = await _cartRepository.GetCartAsync(userId);
		if(cart == null)
		{
			return new Cart { UserId = userId };
		}

		foreach(var item in cart.Items)
		{
			var product = await _productService.GetProductByIdAsync(item.ProductId);
			if(product == null)
			{
				Console.WriteLine($"Product {item.ProductId} not found");
				item.IsAvailable = false;
			}
		}

		return cart;
	}

    // POST: api/cart/{userId}
	[HttpPost("{userId}")]
	public async Task<IActionResult> AddToCart(string userId, [FromBody] CartItem item)
	{
		var product = await _productService.GetProductByIdAsync(item.ProductId);
		if(product == null)
		{
			return BadRequest("Product not found");
		}

		var cart = await _cartRepository.GetCartAsync(userId)
			?? new Cart { UserId = userId };

		var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
		if(existingItem != null)
		{
			existingItem.Quantity += item.Quantity;
			await _cartRepository.SaveCartAsync(cart);
		}
		else
		{
			item.ProductName = product.Name;
			item.UnitPrice = product.Price;
			cart.Items.Add(item);
			await _cartRepository.SaveCartAsync(cart);
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