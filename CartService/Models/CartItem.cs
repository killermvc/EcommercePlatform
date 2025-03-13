namespace CartService.Models;

public class CartItem
{
	public string ProductName { get; set; } = string.Empty;
	public decimal UnitPrice { get; set; }
	public int ProductId { get; set; }
    public int Quantity { get; set; }
	public bool IsAvailable {get; set;} = true;
}