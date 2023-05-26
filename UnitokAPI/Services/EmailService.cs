using System.Net;
using System.Net.Mail;
using Contracts.InterService;
using Microsoft.Extensions.Options;
using Services.Abstraction;

namespace Persistence.Services;

public class EmailService : IEmailService
{

    private readonly MailSettings _settings;

    public EmailService(IOptions<MailSettings> options)
    {
        _settings = options.Value;
    }
    public async Task<bool> SendFeedbackAsync(string mail, string subject, string message)
    {
        SmtpClient smtpServer = new SmtpClient(_settings.Host)
        {
            Port = _settings.Port,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials =
                new NetworkCredential(_settings.Email, _settings.Password),
        };

        MailMessage mailToAdmin = new MailMessage
        {
            From = new MailAddress(_settings.Email),
            Subject = subject,
            IsBodyHtml = true,
            Body = message,
        };
        
        MailMessage mailToUser = new MailMessage
        {
            From = new MailAddress(_settings.Email),
            Subject = subject,
            IsBodyHtml = true,
            Body = message
        };

        try
        {
            mailToAdmin.To.Add(_settings.Email); 
            mailToAdmin.IsBodyHtml = true;
            await smtpServer.SendMailAsync(mailToAdmin);

            mailToUser.To.Add(mail);
            mailToUser.IsBodyHtml = true;
            await smtpServer.SendMailAsync(mailToUser);

            Console.WriteLine("mail Send");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return false;
        }
    }
}