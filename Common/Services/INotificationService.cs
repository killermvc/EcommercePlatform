using Refit;

using Common.DTOs;

namespace Common.Services;

public interface INotificationService
{
	[Post("/notifications")]
	Task SendNotification(NotificationDto notification);
}