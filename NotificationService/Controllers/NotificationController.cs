using Microsoft.AspNetCore.Mvc;

using NotificationService.Models;
using NotificationService.Services;
using Common.DTOs;


namespace NotificationService.Controllers;

[ApiController]
[Route("notifications")]
public class NotificationController(NotificationServiceClass _notificationService) : ControllerBase
{

	[HttpPost("send")]
	public async Task<IActionResult> SendNotification([FromBody] NotificationDto notificationDto)
	{
		var notification = Notification.FromDto(notificationDto);
		await _notificationService.SendNotification(notification);
		return Accepted();
	}
}