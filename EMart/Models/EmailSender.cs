using Microsoft.AspNetCore.Identity.UI.Services;

namespace EMart.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //logic to ssend email
            return Task.CompletedTask;
        }
    }
}
