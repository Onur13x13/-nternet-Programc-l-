using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication2.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Email Gönderme işlemlerini buradan yapıyoruz
            return Task.CompletedTask;
        }
    }
}
