using PaymentService.Data;
using PaymentService.Models;
using Common.DTOs;

namespace PaymentService.Services;

public class PaymentServiceClass(PaymentDbContext _context, IPaymentGateway _paymentGateway)
{

    public async Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            OrderId = request.OrderId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            Status = PaymentStatus.Pending
        };

        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();

        bool success = await _paymentGateway.ProcessPayment(request);
        payment.Status = success ? PaymentStatus.Completed : PaymentStatus.Failed;

        await _context.SaveChangesAsync();

        return new PaymentResponseDto
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Status = payment.Status.ToString()
        };
    }
}