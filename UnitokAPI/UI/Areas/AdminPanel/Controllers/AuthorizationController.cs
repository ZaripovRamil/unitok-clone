using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using UI.Areas.AdminPanel.ViewModel;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class AuthorizationController : Controller
{
    private readonly IAccountService _accountService;

    public AuthorizationController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Authorization([FromForm] LoginVM model)
    {
        if (!ModelState.IsValid) return View(model);
        var loginDto = new LoginDto
        {
            Email = model.Email,
            Password = model.Password
        };
        var loginResult = await _accountService.Login(loginDto);
        if (loginResult.IsSucceeded)
            return RedirectToAction(Url.Action("Profile", "Profile", new { area = "AdminPanel" }));
        ModelState.AddModelError("LoginError", "Invalid credentials");
        return View(model);
    }

    [HttpGet]
    public IActionResult Authorization()
    {
        return View();
    }
}