using Common.DTOs;

namespace PaymentService.Services;

public class FakePaymentGateway : IPaymentGateway
{
    public async Task<bool> ProcessPayment(PaymentRequestDto request)
    {
        await Task.Delay(1000);
        if(request.PaymentMethod == "card")
		{
			return true;
		}
		return false;
    }
}