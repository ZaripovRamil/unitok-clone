using Contracts.BackToFront.Light;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Domain.Accessors.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using UI.Models;
using Services.Abstraction.EntityCRUDServices.EntityCreators;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;

namespace UI.Controllers;

[Route("create")]
public class NFTCreateController : Controller
{
    private readonly ILogger<NFTCreateController> _logger;
    private readonly UserManager<User> _userManager;
    private readonly ITokenCreator _tokenCreator;
    private readonly IUserReader _userReader;
    private readonly IStaticService _staticService;
    private readonly ITokenUpdater _tokenUpdater;
    private readonly IDbTokenAccessor _tokenAccessor;

    public NFTCreateController(ILogger<NFTCreateController> logger,UserManager<User> userManager, ITokenCreator tokenCreator, IUserReader userReader, IStaticService staticService, ITokenUpdater tokenUpdater, IDbTokenAccessor tokenAccessor)
    {
        _logger = logger;
        _userManager = userManager;
        _tokenCreator = tokenCreator;
        _userReader = userReader;
        _staticService = staticService;
        _tokenUpdater = tokenUpdater;
        _tokenAccessor = tokenAccessor;
    }

    [HttpGet]
    public async Task<IActionResult> NFTCreate()
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> NFTCreate([FromForm] TokenCreationDataWithFile tokenCreationDataWithFile)
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        var user = await _userManager.GetUserAsync(User);
        tokenCreationDataWithFile.CreatorId = user.Id;
        var result = await _tokenCreator.Create(tokenCreationDataWithFile);
        if (!result.IsSuccessful)
        {
            ModelState.AddModelError("", result.ResultMessage);
            return View(tokenCreationDataWithFile);
        }

        await _staticService.UploadAsync("NFT", $"{result.TokenId}.jpg",
            tokenCreationDataWithFile.File.OpenReadStream());
        await _tokenAccessor.UpdateFileId(result.TokenId, $"{result.TokenId}.jpg");
        
        return Redirect($"/author/{user.UserName}");
    }
}