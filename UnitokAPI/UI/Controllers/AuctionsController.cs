using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Creation.Codes;
using Domain.Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Auctions;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using UI.Models;

namespace UI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuctionsController : Controller
{
    private readonly IAuctionReader _auctionReader;
    private readonly IAuctionService _auctionService;
    private readonly ITokenReader _tokenReader;
    private readonly UserManager<User> _userManager;

    public AuctionsController(IAuctionReader auctionReader, IAuctionService auctionService, ITokenReader tokenReader, UserManager<User> userManager)
    {
        _auctionReader = auctionReader;
        _auctionService = auctionService;
        _tokenReader = tokenReader;
        _userManager = userManager;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Auction(string id)
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        var auction = await _auctionReader.Read(id);
        if (auction is null) return BadRequest();
        var auctionVm = new AuctionVM
        {
            Id = auction.Id,
            CreatorName = auction.Token.Owner.UserName!,
            CurrentBidPrice = auction.BestPrice,
            StartTime = auction.Start,
            FinishTime = auction.Finish,
            Token = new TokenVM
            {
                Id = auction.Token.Id,
                Name = auction.Token.Name,
                Description = auction.Token.Description,
                FileUrl = auction.Token.FileId,
                TokenType = auction.Token.Type.ToString()
            },
            Bids = auction.Bids.Select(bid => new BidVM
            {
                Id = bid.Id, BidderName = bid.Bidder.UserName!, BidTime = bid.BidTime, BidValue = bid.Price
            })
        };
        return View(auctionVm);
    }

    [HttpPost("create")]
    public async Task<IActionResult> AuctionCreation([FromForm] Test creationData)
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        var data = new AuctionCreationData
        {
            TokenId = creationData.TokenId,
            StartPrice = creationData.StartPrice,
            Duration = TimeSpan.FromMinutes(creationData.Duration)
        };
        var token = await _tokenReader.Read(creationData.TokenId);
        if (token is null)
            return BadRequest(new AuctionCreationResult(AuctionCreationValidationCode.InvalidToken, "").ResultMessage);
        if (token.Owner.Id != (await _userManager.GetUserAsync(User))!.Id)
            return BadRequest("You don't own this token");
        var result = await _auctionService.CreateAuctionAsync(data);
        if (result.IsSuccessful) return Redirect($"/auctions/{result.AuctionId}");
        ModelState.AddModelError("", result.ResultMessage);
        return View(creationData);
    }

    [HttpGet("create")]
    public async Task<IActionResult> AuctionCreation()
    {
        ViewData["User"] = await _userManager.GetUserAsync(User);
        return View();
    }
}

public class Test
{
    public string TokenId { get; set; }

    public decimal StartPrice { get; set; }

    // public TimeSpan Duration { get; set; }
    public int Duration { get; set; }
}