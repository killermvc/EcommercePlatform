using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PaymentService.Data;
using PaymentService.Services;
using Common.DTOs;

namespace PaymentService.Controllers;

[ApiController]
[Route("payments")]
public class PaymentController(PaymentServiceClass _paymentService) : ControllerBase
{

    [HttpPost("pay")]
    public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
    {
        var response = await _paymentService.ProcessPayment(request);
		if(response.Status == PaymentStatus.Completed.ToString())
		{
			response.Success = true;
			return Ok(response);
		}
		else
		{
			response.Success = false;
			return BadRequest(response);
		}
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetPaymentStatus(Guid orderId, [FromServices] PaymentDbContext context)
    {
        var payment = await context.Payments.FirstOrDefaultAsync(p => p.OrderId == orderId);
        if (payment == null) return NotFound("Payment not found");

        return Ok(new PaymentResponseDto
        {
            PaymentId = payment.Id,
            OrderId = payment.OrderId,
            Status = payment.Status.ToString(),
			Success = payment.Status == PaymentStatus.Completed
        });
    }
}
