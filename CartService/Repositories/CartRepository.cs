using System.Text.Json;
using StackExchange.Redis;
using CartService.Models;

namespace CartService.Repositories;

public class CartRepository(IConnectionMultiplexer redis)
{
	private readonly IDatabase _database = redis.GetDatabase();

	public async Task<Cart?> GetCartAsync(string userId)
	{
		var data = await _database.StringGetAsync(userId);
		return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data!);
	}

	public async Task SaveCartAsync(Cart cart)
	{
		var jsonData = JsonSerializer.Serialize(cart);
		await _database.StringSetAsync(cart.UserId, jsonData);
	}

	public async Task DeleteCartAsync(string userId)
	{
		await _database.KeyDeleteAsync(userId);
	}
}
