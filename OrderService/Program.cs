using Refit;
using Microsoft.EntityFrameworkCore;

using OrderService.Data;
using OrderService.Services;
using Common.DTOs;
using Common.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
		new MySqlServerVersion(new Version(8, 0, 40))));

builder.Services.AddScoped<OrderServiceClass>();
builder.Services.AddRefitClient<ICartService>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://cartservice"));
builder.Services.AddRefitClient<IPaymentService>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://paymentservice"));
builder.Services.AddRefitClient<INotificationService>().ConfigureHttpClient(c => c.BaseAddress = new Uri("http://notificationservice"));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
