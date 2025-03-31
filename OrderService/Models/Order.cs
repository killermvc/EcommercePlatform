namespace OrderService.Models;

public class Order
{
	public Guid Id { get; set; }
	public string UserId { get; set; } = string.Empty;
	public decimal TotalAmount { get; set; }
	public OrderStatus Status { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public List<OrderItem> Items { get; set; } = [];
}

public enum OrderStatus
{
	Pending,
	Paid,
	Shipped,
	Delivered,
	Cancelled
}

