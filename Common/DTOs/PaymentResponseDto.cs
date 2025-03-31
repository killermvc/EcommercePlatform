namespace Common.DTOs;

public class PaymentResponseDto
{
    public Guid PaymentId { get; set; }
    public Guid OrderId { get; set; }
    public string Status { get; set; } = string.Empty;
	public bool Success { get; set; }
}