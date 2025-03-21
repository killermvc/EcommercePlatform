using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NotificationService.Services;

public class EmailService(ILogger<EmailService> _logger, IConfiguration _configuration)
{

	public async Task SendEmailAsync(string recipient, string message)
	{
		var smtpHost = _configuration["SMTP:Host"]!;
		var smtpPort = int.Parse(_configuration["SMTP:Port"]!);
		var smtpUser = _configuration["SMTP:User"]!;
		var smtpPass = _configuration["SMTP:Password"]!;
		var fromEmail = _configuration["SMTP:From"]!;

		using var client = new SmtpClient(smtpHost, smtpPort)
		{
			Credentials = new NetworkCredential(smtpUser, smtpPass),
			EnableSsl = true
		};

		var mailMessage = new MailMessage(fromEmail, recipient, "Notification", message);

		try
		{
			await client.SendMailAsync(mailMessage);
			_logger.LogInformation($"Email sent to {recipient}");
		}
		catch (SmtpException ex)
		{
			_logger.LogError($"Failed to send email to {recipient}: {ex.Message}");
		}
	}
}
