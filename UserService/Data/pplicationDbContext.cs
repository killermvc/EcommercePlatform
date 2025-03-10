using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	: IdentityDbContext<ApplicationUser>(options)
{

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		// Seed roles (optional)
		builder.Entity<IdentityRole>().HasData(
			new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
			new IdentityRole { Name = "User", NormalizedName = "USER" }
		);
	}
}