using Common.DTOs;

namespace PaymentService.Services;

public interface IPaymentGateway
{
    Task<bool> ProcessPayment(PaymentRequestDto request);
}