using BusinessLogic.Services.Implementations;
using BusinessLogic.Services.Interfaces;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IEmailService> _lazyEmailService;

    public ServiceManager(IEmailService emailService)
    {
        _lazyEmailService = new Lazy<IEmailService>(emailService);
    }

    public IEmailService EmailService => _lazyEmailService.Value;
}