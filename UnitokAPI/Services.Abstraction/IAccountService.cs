using Contracts;

namespace Services.Abstraction;

public interface IAccountService
{
    public Task<AuthDto> Register(RegisterDto registerDto);
    public Task<AuthDto> Login(LoginDto loginDto);
    public Task<bool> ConfirmEmail(string userId, string code);
    public Task HandleForgotPassword(string email);
    public Task<bool> ResetPassword(string userId, string validationToken, string newPassword);
    public Task Logout();
}
