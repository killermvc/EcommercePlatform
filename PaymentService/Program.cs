using Microsoft.EntityFrameworkCore;

using PaymentService.Data;
using PaymentService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseMySql(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		new MySqlServerVersion(new Version(8, 0, 40))));

builder.Services.AddScoped<PaymentServiceClass>();
builder.Services.AddScoped<IPaymentGateway, FakePaymentGateway>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

using (var scope = app.Services.CreateScope())
{

    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();

	dbContext.Database.EnsureCreated();

	if(dbContext.Database.GetPendingMigrations().Any())
	{
		dbContext.Database.Migrate();
	}
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
