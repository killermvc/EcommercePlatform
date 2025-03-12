using System.Net.Http.Json;

using CartService.Models;
using Common.DTOs;

namespace CartService.Services;

public class ProductServiceClient(HttpClient _httpClient, IConfiguration _configuration)
{

	public async Task<ProductDto?> GetProductByIdAsync(int productId)
	{
		var productServiceUrl = _configuration["ProductService:BaseUrl"];
		return await _httpClient.GetFromJsonAsync<ProductDto>($"{productServiceUrl}/api/products/{productId}");
	}
}