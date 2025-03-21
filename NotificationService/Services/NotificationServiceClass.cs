using NotificationService.Models;
using NotificationService.Repositories;
using System.Threading.Tasks;

namespace NotificationService.Services;

public class NotificationServiceClass
	(NotificationRepository _repository, EmailService _emailService, SmsService _smsService)
{

	public async Task SendNotification(Notification notification)
	{
		// Simulate sending notification
		if (notification.Type == "Email")
		{
			await _emailService.SendEmailAsync(notification.UserId, notification.Message);
		}
		else if (notification.Type == "SMS")
		{
			await _smsService.SendSmsAsync(notification.UserId, notification.Message);
		}

		notification.Status = "Sent";
		await _repository.UpdateNotification(notification);
	}
}

