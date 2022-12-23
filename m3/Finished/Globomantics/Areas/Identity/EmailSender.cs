using Microsoft.AspNetCore.Identity.UI.Services;

namespace Globomantics.Areas.Identity
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //SmtpClient
            //SendGrid
            return Task.CompletedTask;
        }
    }
}
