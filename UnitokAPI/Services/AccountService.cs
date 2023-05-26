using System.Text;
using Contracts;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Services.Abstraction;

namespace Services;

public class AccountService : IAccountService
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
    }

    public async Task<AuthDto> Register(RegisterDto registerDto)
    {
        var user = new User()
        {
            UserName = registerDto.Name,
            Email = registerDto.Email,
            Role = Role.User,
            CreatedTokens = new List<Token>(),
            OwnedTokens = new List<Token>(),
            Wallet = Guid.NewGuid().ToString(),
            Balance = 0
        };
        var userResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!userResult.Succeeded)
            return new AuthDto
            {
                IsSucceeded = userResult.Succeeded, Errors = userResult.Errors.Select(error => error.Description)
            };
        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var callback =
            $"https://localhost:7177/confirmEmail?userId={userId}&code={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code))}";
        var emailResult = await _emailService.SendFeedbackAsync(user.Email, "Email confirmation",
            $"Hey user {user.UserName}! Please click the link to confirm your email: <a href=\"{callback}\">{callback}</a>");
        // if (!emailResult) TODO: somfin cleva if fails
        // await _signInManager.SignInAsync(user, false);
        return new AuthDto
        {
            IsSucceeded = userResult.Succeeded, Errors = userResult.Errors.Select(error => error.Description)
        };
    }

    public async Task<bool> ConfirmEmail(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return false;
        var result =
            await _userManager.ConfirmEmailAsync(user, Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)));
        return result.Succeeded;
    }
    
    public async Task HandleForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !user.EmailConfirmed) return;
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var callback =
            $"https://localhost:7177/resetPassword?userId={user.Id}&code={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))}";
        var emailResult = await _emailService.SendFeedbackAsync(user.Email, "Password reseting",
            $"Someone is trying to reset a password for you account. If it is not you, do not click the following link! <a href=\"{callback}\">{callback}</a>");
        // if (!emailResult) TODO: somfin cleva if fails
    }

    public async Task<bool> ResetPassword(string userId, string token, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null || !user.EmailConfirmed) return false;
        var result = await _userManager.ResetPasswordAsync(user,
            Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)), newPassword);
        return result.Succeeded;
    }
    public async Task<AuthDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            return new AuthDto { IsSucceeded = false, Errors = new List<string> { "User doesn't exist" } };
        if (!user.EmailConfirmed)
            return new AuthDto { IsSucceeded = false, Errors = new List<string> { "Email is not confirmed" } };
        var userResult = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        return new AuthDto
        {
            IsSucceeded = userResult.Succeeded,
            Errors = userResult.Succeeded ? new List<string>() : new List<string> { "Sign-In failed" }
        };
    }
    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }
}