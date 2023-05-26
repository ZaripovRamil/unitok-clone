using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;

namespace UI.Controllers;

[Route("contact")]
public class ContactsController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IEmailService _emailService;
    private readonly UserManager<User> _userManager;

    public ContactsController(ILogger<HomeController> logger, IEmailService emailService, UserManager<User> userManager)
    {
        _logger = logger;
        _emailService = emailService;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Contacts()
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        return View();
    }

    [HttpPost("sendFeedback")]
    public async Task<IActionResult> SendFeedback([FromForm] FeedbackCreationData feedback)
    {
        var isSend = await _emailService.SendFeedbackAsync(feedback.Email, feedback.Subject, feedback.Message);
        return Redirect("/contact");
    }
}