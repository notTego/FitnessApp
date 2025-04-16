using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace FitnessTracker.Helpers
{
	public class EmailService
	{
		public async Task SendAsync(string toEmail, string subject, string body)
		{
			var email = new MimeMessage();
			email.From.Add(MailboxAddress.Parse("demo@fitnessapp.com"));
			email.To.Add(MailboxAddress.Parse(toEmail));
			email.Subject = subject;
			email.Body = new TextPart(TextFormat.Html) { Text = body };

			using var smtp = new SmtpClient();
			await smtp.ConnectAsync("smtp.mailtrap.io", 587, false);
			await smtp.AuthenticateAsync("YOUR_MAILTRAP_USERNAME", "YOUR_MAILTRAP_PASSWORD");
			await smtp.SendAsync(email);
			await smtp.DisconnectAsync(true);
		}
	}
}
