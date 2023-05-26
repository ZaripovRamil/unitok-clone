
using System.Text.Json;
using Contracts;
using Contracts.BackToFront.Light.Entities;

using Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;
using Services.Abstraction.LogReaders;
using UI.Models;

namespace UI.Controllers;

[ApiController]
[Route("/author")]
public class ProfileController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDistributedCache _cache;
    private readonly UserManager<User> _userManager;
    private readonly IUserReader _userReader;
    private readonly IUserUpdater _userUpdater;
    private readonly ILogReader _logReader;
    private readonly IStaticService _staticService;

    [BindProperty] public string UserName { get; set; }

    public ProfileController(ILogger<HomeController> logger, UserManager<User> userManager, IUserReader userReader,
        ILogReader logReader, IUserUpdater userUpdater, IStaticService staticService, IDistributedCache cache)
    {
        _userManager = userManager;
        _logger = logger;
        _userReader = userReader;
        _userUpdater = userUpdater;
        _logReader = logReader;
        _staticService = staticService;
        _cache = cache;
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> UserProfile(string username)
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        bool isAuthorized = false;
        var authorizedUserName = _userManager.GetUserName(User);
        if (authorizedUserName != null)
        {
            if (authorizedUserName == username)
            {
                isAuthorized = true;
                var userAuthorized = await _userReader.ReadAuthorizedByUsername(authorizedUserName);
                if (userAuthorized != null)
                    return View("UserProfile", new UserProfileVM()
                    {
                        Email = userAuthorized.Email,
                        ProfilePicUrl = userAuthorized.ProfilePicUrl,
                        OnAuction = new List<AuctionLight>(),
                        Nickname = userAuthorized.UserName,
                        FirstName = userAuthorized.FirstName,
                        LastName = userAuthorized.LastName,
                        Wallet = userAuthorized.Wallet,
                        OwnedTokens = userAuthorized.OwnedTokens,
                        Balance = userAuthorized.Balance,
                        CreatedTokens = userAuthorized.CreatedTokens,
                        AuctionFinishLogs = _logReader.AuctionFinishLogReader.GetByUsername(userAuthorized.UserName),
                        AuctionParticipationLogs =
                            _logReader.AuctionParticipationLogReader.GetByUsername(userAuthorized.UserName),
                        TokenCreationLogs = _logReader.TokenCreationLogReader.GetByUsername(userAuthorized.UserName),
                        IsAuthorized = isAuthorized
                    });
            }
            else
            {
                isAuthorized = false;
                var user = await _userReader.ReadByUsername(username);
                return View("UserProfile",new UserProfileVM()
                    {
                        Email = "",
                        ProfilePicUrl = user.ProfilePicUrl,
                        OnAuction = new List<AuctionLight>(),
                        Nickname = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Wallet = user.Wallet,
                        OwnedTokens = user.OwnedTokens,
                        Balance = user.Balance,
                        CreatedTokens = user.CreatedTokens,
                        AuctionFinishLogs = _logReader.AuctionFinishLogReader.GetByUsername(user.UserName),
                        AuctionParticipationLogs = _logReader.AuctionParticipationLogReader.GetByUsername(user.UserName),
                        TokenCreationLogs = _logReader.TokenCreationLogReader.GetByUsername(user.UserName),
                        IsAuthorized = isAuthorized
                    });
            }
        }
        else
        {
            var user = await _userReader.ReadByUsername(username);
            if (user != null)
                return View("UserProfile", new UserProfileVM()
                {
                    ProfilePicUrl = user.ProfilePicUrl,
                    OnAuction = new List<AuctionLight>(),
                    Nickname = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    OwnedTokens = user.OwnedTokens,
                    CreatedTokens = user.CreatedTokens,
                    AuctionFinishLogs = _logReader.AuctionFinishLogReader.GetByUsername(user.UserName),
                    AuctionParticipationLogs = _logReader.AuctionParticipationLogReader.GetByUsername(user.UserName),
                    TokenCreationLogs = _logReader.TokenCreationLogReader.GetByUsername(user.UserName),
                });
        }

        return Redirect("/404");
        // return new NotFoundResult();
    }

    [Authorize]
    [HttpPost("changeDetails")]
    public async Task<IActionResult> ChangeProfileDetails([FromForm] ChangeProfileDetailsVm data)
    {
        Console.WriteLine("change profile details");
        var user = await _userManager.GetUserAsync(User);
        await _cache.SetStringAsync(data.UserName,
            JsonSerializer.Serialize(new UserPersonalInfoDto
            {
                Email = user.Email,
                UserName = data.UserName,
                FirstName = data.FirstName,
                LastName = data.LastName
            }));
        Console.WriteLine(await _cache.GetStringAsync(data.UserName));
        var res = await _userUpdater.Update(new ChangeProfileDetailsData()
        {
            Id = user.Id,
            FirstName = data.FirstName,
            LastName = data.LastName,
            UserName = data.UserName
        });

        if (!res.IsSuccessful)
        {
            ModelState.AddModelError("Details", res.ResultMessage);
            return await UserProfile(user.UserName);
        }
        else
        {
            return Redirect($"/author/{User.Identity.Name}");
        }
    }

    [Authorize]
    [HttpPost("changePassword")]
    public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordVm data)
    {
        Console.WriteLine("change password");
        var user = await _userManager.GetUserAsync(User);
        var res = await _userUpdater.Update(new ChangePasswordData()
        {
            Id = user.Id,
            ComfirmPassword = data.ConfirmPassword,
            OldPassword = data.UserName,
            NewPassword = data.NewPassword
        });
        if (!res.IsSuccessful)
        {
            ModelState.AddModelError("Password", res.ResultMessage);
            return await UserProfile(user.UserName);
        }

        return Redirect($"/author/{User.Identity.Name}");
    }

    [Authorize]
    [HttpPost("changeBalance")]
    public async Task<IActionResult> ReplenishBalance([FromForm] ChangeBalanceDataVm data)
    {
        Console.WriteLine("change balance");
        var user = await _userManager.GetUserAsync(User);
        var res = await _userUpdater.Update(new ChangeBalanceData()
        {
            Id = user.Id,
            Amount = data.Amount,
            Seed = data.Seed,
            Wallet = data.UserName
        });
        if (!res.IsSuccessful)
        {
            ModelState.AddModelError("Balance", res.ResultMessage);
            return await UserProfile(user.UserName);
        }

        return Redirect($"/author/{User.Identity.Name}");
    }
    [HttpGet]
    public IActionResult GetProfilePicture(string pictureUrl)
    {
        var profilePic = _staticService.GetFileAsStream("ProfilePicture", $"{pictureUrl}");
        if (profilePic is null) return NotFound();
        return File(profilePic, "image/jpeg");
    }
}