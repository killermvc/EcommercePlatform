namespace Common.DTOs;

public class PaymentRequestDto
{
	public string UserId { get; set; } = string.Empty;
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = string.Empty; // e.g., "CreditCard", "PayPal"
}