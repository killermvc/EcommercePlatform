using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace NotificationService.Services;

public class SmsService(ILogger<SmsService> _logger, IConfiguration _configuration)
{

	public async Task SendSmsAsync(string recipient, string message)
	{
		var accountSid = _configuration["Twilio:AccountSid"];
		var authToken = _configuration["Twilio:AuthToken"];
		var fromNumber = _configuration["Twilio:From"];

		TwilioClient.Init(accountSid, authToken);

		try
		{
			var messageResult = await MessageResource.CreateAsync(
				body: message,
				from: new Twilio.Types.PhoneNumber(fromNumber),
				to: new Twilio.Types.PhoneNumber(recipient)
			);

			_logger.LogInformation("SMS sent to {recipient}, SID: {messageResult.Sid}", recipient, messageResult.Sid);
		}
		catch (System.Exception ex)
		{
			_logger.LogError("Failed to send SMS to {recipient}: {message}", recipient, ex.Message);
		}
	}
}
