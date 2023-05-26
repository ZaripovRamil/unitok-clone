using Models.Dto.FrontToBack.EntityCreationData;

namespace BusinessLogic.Services.Interfaces;

public interface IEmailService
{
    void SendFeedbackEmail(FeedbackData feedback);
}