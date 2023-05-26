using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;
[Route("404")]
public class NotFoundController: Controller
{
    private readonly ILogger<PrivacyPolicyController> _logger;
    private readonly UserManager<User> _userManager;

    public NotFoundController(ILogger<PrivacyPolicyController> logger,UserManager<User> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    
    [HttpGet]
    public async Task<IActionResult> NotFound()
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        return View();
    }
}