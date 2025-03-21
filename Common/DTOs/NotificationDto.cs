namespace Common.DTOs;

public class NotificationDto
{
	public int Id { get; set; }
	public string UserId { get; set; } = string.Empty;
	public string Type { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public string Status { get; set; } = string.Empty;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}