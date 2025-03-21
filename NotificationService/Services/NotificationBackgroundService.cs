using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NotificationService.Services;

public class NotificationBackgroundService
(
	IServiceScopeFactory _scopeFactory,
	ILogger<NotificationBackgroundService> _logger
) : BackgroundService
{
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var repository = scope.ServiceProvider.GetRequiredService<NotificationRepository>();
				var service = scope.ServiceProvider.GetRequiredService<NotificationServiceClass>();

				var pendingNotifications = await repository.GetPendingNotifications();
				foreach (var notification in pendingNotifications)
				{
					_logger.LogInformation("Processing notification {NotificationId}", notification.Id);
					await service.SendNotification(notification);
				}
			}

			await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken); // Run every 10 seconds
		}
	}
    }

