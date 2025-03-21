using Microsoft.EntityFrameworkCore;
using NotificationService.Data;
using NotificationService.Models;

namespace NotificationService.Repositories;

public class NotificationRepository
{
	private readonly AppDbContext _context;

	public NotificationRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task AddNotification(Notification notification)
	{
		_context.Notifications.Add(notification);
		await _context.SaveChangesAsync();
	}

	public async Task<List<Notification>> GetPendingNotifications()
	{
		return await _context.Notifications
			.Where(n => n.Status == "Pending")
			.ToListAsync();
	}

	public async Task UpdateNotification(Notification notification)
	{
		_context.Notifications.Update(notification);
		await _context.SaveChangesAsync();
	}
}

