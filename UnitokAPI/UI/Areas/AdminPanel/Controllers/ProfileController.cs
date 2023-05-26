using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using UI.Areas.AdminPanel.ViewModel;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IStaticService _staticService;

    public ProfileController(UserManager<User> userManager, IStaticService staticService)
    {
        _userManager = userManager;
        _staticService = staticService;
    }

    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user is null)
            return RedirectToAction(Url.Action("Authorization", "Authorization", new { area = "AdminPanel" }));
        var userRole = user.Role switch
        {
            Role.Admin => "Admin",
            Role.User => "User",
            _ => "Unknown"
        };
        var userVm = new UserVM
        {
            Email = user.Email,
            FullName = user.FullName,
            Mobile = user.PhoneNumber,
            Wallet = user.Wallet,
            Role = userRole,
            ProfilePicture = user.ProfilePicUrl
        };
        return View(userVm);
    }

    public IActionResult GetProfilePicture(string pictureUrl)
    {
        var profilePic = _staticService.GetFileAsStream("ProfilePicture", $"{pictureUrl}");
        if (profilePic is null) return NotFound();
        return File(profilePic, "image/jpeg");
    }
}