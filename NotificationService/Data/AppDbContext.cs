using Microsoft.EntityFrameworkCore;
using NotificationService.Models;

namespace NotificationService.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
		: DbContext(options)
    {

        public DbSet<Notification> Notifications { get; set; } = null!;
    }
}