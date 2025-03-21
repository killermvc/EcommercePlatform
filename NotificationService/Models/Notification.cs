using Common.DTOs;

namespace NotificationService.Models;

public class Notification
{
	public int Id { get; set; }
	public string UserId { get; set; } = string.Empty;  // Recipient user ID
	public string Type { get; set; } = string.Empty;    // "Email", "SMS", "Push"
	public string Message { get; set; } = string.Empty;
	public string Status { get; set; } = string.Empty;  // "Pending", "Sent", "Failed"
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public static Notification FromDto(NotificationDto notification)
	{
		return new Notification
		{
			UserId = notification.UserId,
			Type = notification.Type,
			Message = notification.Message,
			Status = notification.Status,
			CreatedAt = notification.CreatedAt
		};
	}

	public NotificationDto ToDto()
	{
		return new NotificationDto
		{
			Id = Id,
			UserId = UserId,
			Type = Type,
			Message = Message,
			Status = Status,
			CreatedAt = CreatedAt
		};
	}
}