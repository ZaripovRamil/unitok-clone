using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using UI.Areas.AdminPanel.ViewModel;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class RegistrationController : Controller
{
    private readonly IAccountService _accountService;

    public RegistrationController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Registration()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registration([FromForm] RegisterVM model)
    {
        if (!ModelState.IsValid) return View(model);
        var registerDto = new RegisterDto
        {
            Email = model.Email,
            Name = model.Name,
            Password = model.Password
        };
        var registerResult = await _accountService.Register(registerDto);
        if (registerResult.IsSucceeded)
            RedirectToAction(Url.Action("Authorization", "Authorization", new { area = "AdminPanel" }));
        foreach (var error in registerResult.Errors) ModelState.AddModelError(string.Empty, error);
        return View(model);
    }
}