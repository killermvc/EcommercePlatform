namespace CartService.Models;

public class Cart
{
	public string UserId { get; set; } = string.Empty;
	public List<CartItem> Items { get; set; } = [];
}
