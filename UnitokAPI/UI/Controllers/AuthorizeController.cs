using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using UI.Models;

namespace UI.Controllers;

public class AuthorizeController : Controller
{
    private readonly ILogger<AuthorizeController> _logger;
    private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;

    public AuthorizeController(ILogger<AuthorizeController> logger, IAccountService accountService,
        UserManager<User> userManager)
    {
        _logger = logger;
        _accountService = accountService;
        _userManager = userManager;
    }

    [HttpGet("/signIn")]
    public IActionResult SignIn()
    {
        return View();
    }

    [HttpGet("/signUp")]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost("/signUp")]
    public async Task<IActionResult> SignUp([FromForm] RegisterFormVM model)
    {
        if (!ModelState.IsValid) return View(model);
        var registerResult = await _accountService.Register(new RegisterDto()
        {
            Email = model.Email,
            Name = model.Name,
            Password = model.Password
        });

        foreach (var error in registerResult.Errors)
        {
            if (error.Contains("Username"))
            {
                ModelState.AddModelError("Name", error);
            }
            else if (error.Contains("Email"))
            {
                ModelState.AddModelError("Email", error);
            }
            else if (error.Contains("Passwords"))
            {
                ModelState.AddModelError("Password", error);
            }
            else
            {
                ModelState.AddModelError("", error);
            }
        }

        if (registerResult.IsSucceeded)
            return RedirectToAction("Email", "Authorize",
                new
                {
                    message =
                        "Your account has been successfully registered. To complete the process please check your email for a validation request."
                });
        return View(model);
    }

    [HttpPost("/signIn")]
    public async Task<IActionResult> SignIn([FromForm] LoginFormVM model)
    {
        if (!ModelState.IsValid) return View(model);
        var loginResult = await _accountService.Login(new LoginDto()
        {
            Email = model.Email,
            Password = model.Password
        });
        if (loginResult.IsSucceeded)
        {
            // var user = await _userManager.GetUserAsync(User);
            return Redirect($"~/");
        }

        ModelState.AddModelError("", "Invalid credentials");
        return View(model);
    }

    [HttpGet("/confirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
    {
        var successful = await _accountService.ConfirmEmail(userId, code);
        // var userName = _userManager.GetUserName(User);
        return successful ? Redirect($"~/signIn") : BadRequest("Email confirmation error");
    }

    [HttpGet("/email")]
    public async Task<IActionResult> Email(string message)
    {
        var model = new EmailVM() { Message = message };
        return View(model);
    }

    [HttpGet("/signout")]
    public async Task<IActionResult> SignOut()
    {
        await _accountService.Logout();
        return Redirect("~/");
    }

    [HttpGet("/forgotPassword")]
    public async Task<IActionResult> Forgot()
    {
        return View();
    }

    [HttpPost("/forgotPassword")]
    public async Task<IActionResult> Forgot([FromForm] ForgotVM model)
    {
        if (!ModelState.IsValid) return View(model);
        await _accountService.HandleForgotPassword(model.Email);
        return RedirectToAction("Email", "Authorize",
            new { Message = "Please check your email to reset the password." });
    }

    [HttpGet("/resetPassword")]
    public async Task<IActionResult> ResetPassword([FromQuery] string userId, [FromQuery] string code)
    {
        var model = new ResetPasswordVM() { UserId = userId, Code = code };
        return View(model);
    }

    [HttpPost("/resetPassword")]
    public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordVM model)
    {
        if (!ModelState.IsValid) return View(model);
        var resetResult = await _accountService.ResetPassword(model.UserId, model.Code, model.Password);
        return resetResult ? Redirect($"~/signIn") : BadRequest("Password reset error");
    }
}