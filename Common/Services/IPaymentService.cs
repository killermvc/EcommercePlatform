using Refit;

using Common.DTOs;

namespace Common.Services;

public interface IPaymentService
{
	[Post("/payments")]
	Task<PaymentResponseDto> ProcessPayment(PaymentRequestDto request);
}