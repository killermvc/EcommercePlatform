using Microsoft.EntityFrameworkCore;

using ProductService.Models;

namespace ProductService.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Product> Products { get; set; } = null!;
	public DbSet<Category> Categories { get; set; } = null!;
}