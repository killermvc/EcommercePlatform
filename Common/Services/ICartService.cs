using Refit;

using Common.DTOs;

namespace Common.Services;
public interface ICartService
{
	[Get("/cart/{userId}")]
	Task<List<CartItemDto>> GetCartItems(string userId);

	[Delete("/cart/{userId}")]
	Task ClearCart(string userId);
}