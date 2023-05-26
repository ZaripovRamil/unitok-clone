using System.Net;
using System.Net.Mail;
using System.Web;
using BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Options;
using Models.Dto.FrontToBack.EntityCreationData;

namespace BusinessLogic.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly MailSettings _settings;

    public EmailService(IOptions<MailSettings> options)
    {
        _settings = options.Value;
    }
    
    public void SendFeedbackEmail(FeedbackData feedback)
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
        
        
        MailMessage feedbackToAdminEmail = new MailMessage
        {
            From = new MailAddress(_settings.Email),
            Subject = feedback.Subject,
            IsBodyHtml = true,
            Body = $"<h4>Пользователь {feedback.Name} (email {feedback.Email}) оставил(а) сообщение на сайте</h4><p>{feedback.Message}</p>"
        };

        try
        {
            feedbackToAdminEmail.To.Add(_settings.Email);
            smtpServer.Send(feedbackToAdminEmail);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}