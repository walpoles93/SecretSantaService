using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SecretSantaService.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace SecretSantaService.Infrastructure.Email
{
    public class Emailer : IEmailer
    {
        private readonly ISmtpClient _smtpClient;
        private readonly EmailSettings _emailSettings;

        public Emailer(ISmtpClient smtpClient, IOptions<EmailSettings> emailSettings)
        {
            _smtpClient = smtpClient ?? throw new ArgumentNullException(nameof(smtpClient));
            _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        }

        public async Task Send(string to, string subject, string body, bool isHtmlBody = true)
        {
            var email = CreateEmail(to, subject, body, isHtmlBody);

            await _smtpClient.ConnectAsync(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await _smtpClient.AuthenticateAsync(_emailSettings.SmtpUser, _emailSettings.SmtpPassword);
            await _smtpClient.SendAsync(email);
            await _smtpClient.DisconnectAsync(true);
        }

        private MimeMessage CreateEmail(string to, string subject, string body, bool isHtmlBody)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailSettings.FromAddress));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(isHtmlBody ? TextFormat.Html : TextFormat.Plain) { Text = body };

            return email;
        }
    }
}
