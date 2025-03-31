using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using OrderService.Data;
using OrderService.Models;
using Common.DTOs;
using Common.Services;

namespace OrderService.Services;

public class OrderServiceClass(AppDbContext _context, ICartService _cartService,
	IPaymentService _paymentService, INotificationService _notificationService,
	ILogger<OrderServiceClass> _logger)
{

	public async Task<Order> CreateOrder(string userId)
	{
		// Get cart items
		var cartItems = await _cartService.GetCartItems(userId);
		if (cartItems.Count == 0)
		{
			throw new Exception("Cart is empty");
		}

		// Calculate total
		decimal total = cartItems.Sum(i => i.Quantity * 10);

		// Create order
		var order = new Order
		{
			UserId = userId,
			TotalAmount = total,
			Status = OrderStatus.Pending,
			Items = cartItems.Select(i => new OrderItem
			{
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				Price = 10 // Mock price
			}).ToList()
		};

		_context.Orders.Add(order);
		await _context.SaveChangesAsync();

		// Process payment
		var paymentRequest = new PaymentRequestDto { UserId = userId, Amount = total, OrderId = order.Id };
		var paymentResult = await _paymentService.ProcessPayment(paymentRequest);

		if (!paymentResult.Success)
		{
			throw new Exception("Payment failed");
		}

		// Update order status
		order.Status = OrderStatus.Paid;
		await _context.SaveChangesAsync();

		// Send notification
		await _notificationService.SendNotification(new NotificationDto
		{
			UserId = userId,
			Message = $"Your order #{order.Id} has been placed successfully!"
		});

		// Clear cart
		await _cartService.ClearCart(userId);

		return order;
	}
}
