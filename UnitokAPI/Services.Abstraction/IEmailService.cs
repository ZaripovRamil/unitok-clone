namespace Services.Abstraction;

public interface IEmailService
{
    public Task<bool> SendFeedbackAsync(string mail, string subject, string message);
}