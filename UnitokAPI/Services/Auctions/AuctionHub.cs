using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Services.Abstraction.Auctions;
using Services.Abstraction.EntityCRUDServices.EntityReaders;

namespace Services.Auctions;

public class AuctionHub : Hub
{
    private readonly IAuctionReader _auctionReader;
    private readonly UserManager<User> _userManager;
    private readonly IAuctionService _auctionService; 

    public AuctionHub(IAuctionReader auctionReader, UserManager<User> userManager, IAuctionService auctionService)
    {
        _auctionReader = auctionReader;
        _userManager = userManager;
        _auctionService = auctionService;
    }

    public async Task<AuctionActionDto> JoinAuction(string auctionId)
    {
        var userResult = await ValidateUser();
        if (!userResult.IsSuccessful) return userResult;
        var auctionResult = await ValidateAuction(auctionId);
        if (!auctionResult.IsSuccessful) return auctionResult;
        await Groups.AddToGroupAsync(Context.ConnectionId, auctionId);
        return new AuctionActionDto(true, "Successful");
    }

    public async Task<AuctionActionDto> PlaceBid(string auctionId, decimal amount)
    {
        var user = await _userManager.GetUserAsync(Context.User);
        if (user is null)
            return new AuctionActionDto(false, "Unauthorized");
        var result = await _auctionService.PlaceBidAsync(auctionId, user.Id, amount);
        if (!result.IsSuccessful) return new AuctionActionDto(result.IsSuccessful, result.ResultMessage);
        await Clients.Group(auctionId)
            .SendAsync("ReceiveBid", new { Name = user.UserName, BidValue = amount, BidderId = user.Id });
        return new AuctionActionDto(result.IsSuccessful, result.ResultMessage);
    }

    public async Task<AuctionActionDto> SendBidsHistory(string auctionId)
    {
        var userResult = await ValidateUser();
        if (!userResult.IsSuccessful) return userResult;
        var auction = await _auctionReader.Read(auctionId);
        if (auction is null || auction.Finish <= DateTime.Now)
            return new AuctionActionDto(false, "Invalid auction");
        
        foreach (var bid in auction.Bids)
            await Clients.Caller.SendAsync("ReceiveBid",
                new { Name = bid.Bidder.UserName, BidValue = bid.Price, BidderId = bid.Bidder.Id });
        
        return new AuctionActionDto(true, "Successful");
    }

    private async Task<AuctionActionDto> ValidateUser()
    {
        var user = await _userManager.GetUserAsync(Context.User);
        return user is null
            ? new AuctionActionDto(false, "Unauthorized")
            : new AuctionActionDto(true, "Successful");
    }

    private async Task<AuctionActionDto> ValidateAuction(string auctionId)
    {
        var auction = await _auctionReader.Read(auctionId);
        return auction is null || auction.Finish <= DateTime.Now
            ? new AuctionActionDto(false, "Invalid auction")
            : new AuctionActionDto(true, "Successful");
    }
}

public class AuctionActionDto
{
    public bool IsSuccessful { get; set; }
    public string ResultMessage { get; set; }

    public AuctionActionDto(bool isSuccessful, string resultMessage)
    {
        IsSuccessful = isSuccessful;
        ResultMessage = resultMessage;
    }
}