using Common.DTOs;

namespace PaymentService.Services;

public class FakePaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPayment(PaymentRequestDto request)
    {
        await Task.Delay(1000);
        return new Random().Next(0, 2) == 1;
    }
}