using System.Text.Json;
using StackExchange.Redis;
using CartService.Models;

namespace CartService.Repositories;

public class CartRepository(IConnectionMultiplexer redis)
{
	private readonly IDatabase _database = redis.GetDatabase();

	public async Task<Cart?> GetCartAsync(string userId)
	{
		var key = $"cart:{userId}";
		var data = await _database.StringGetAsync(key);
		return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data!);
	}

	public async Task SaveCartAsync(Cart cart)
	{
		var jsonData = JsonSerializer.Serialize(cart);
		var key = $"cart:{cart.UserId}";
		await _database.StringSetAsync(key, jsonData);
	}

	public async Task DeleteCartAsync(string userId)
	{
		var key = $"cart:{userId}";
		await _database.KeyDeleteAsync(key);
	}
}
