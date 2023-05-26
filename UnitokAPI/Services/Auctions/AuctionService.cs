using System.Collections.Concurrent;
using Contracts.BackToFront.EntityCRUDResults.EntityCreationResults;
using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;
using Contracts.BackToFront.Full.Entities;
using Contracts.FrontToBack.EntityCRUDData.EntityCreationData;
using Contracts.InterService.EntityValidationResults.Update.Codes;
using Domain.Accessors.Entities;
using Domain.Accessors.EntityOperations;
using Domain.ActivityLogs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction.Auctions;
using Services.Abstraction.EntityCRUDServices.EntityCreators;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using Services.Abstraction.EntityCRUDServices.EntityUpdaters;

namespace Services.Auctions;

public class AuctionService : IAuctionService
{
    public static ConcurrentDictionary<string, AuctionFull> RunningAuctions { get; } = new();

    private readonly IAuctionCreator _auctionCreator;
    private readonly IUserReader _userReader;
    private readonly IAuctionReader _auctionReader;
    private readonly IAuctionUpdater _auctionUpdater;
    private readonly IUserUpdater _userUpdater;
    private readonly ITokenUpdater _tokenUpdater;
    private readonly IDbAuctionAccessor _auctionAccessor;
    private readonly IDbAuctionParticipationLogAccessor _participationLogAccessor;
    private readonly IDbAuctionFinishLogAccessor _finishLogAccessor;

    public AuctionService(IAuctionUpdater auctionUpdater, IUserReader userReader, IUserUpdater userUpdater,
        IAuctionReader auctionReader, ITokenUpdater tokenUpdater, IAuctionCreator auctionCreator,
        ITokenReader tokenReader, UserManager<User> userManager,
        IDbAuctionParticipationLogAccessor participationLogAccessor,
        IDbAuctionFinishLogAccessor finishLogAccessor, IDbAuctionAccessor auctionAccessor)
    {
        _auctionUpdater = auctionUpdater;
        _userReader = userReader;
        _userUpdater = userUpdater;
        _auctionReader = auctionReader;
        _tokenUpdater = tokenUpdater;
        _auctionCreator = auctionCreator;
        _participationLogAccessor = participationLogAccessor;
        _finishLogAccessor = finishLogAccessor;
        _auctionAccessor = auctionAccessor;
    }

    public async Task<AuctionBidAdditionResult> PlaceBidAsync(string auctionId, string userId, decimal bidValue)
    {
        var user = await _userReader.Read(userId);
        if (user is null) return new AuctionBidAdditionResult(BidAdditionValidationCode.NoSuchUser);
        var auction = await _auctionReader.Read(auctionId);
        if (auction is null) return new AuctionBidAdditionResult(BidAdditionValidationCode.NoSuchAuction);
        if (auction.Finish <= DateTime.Now)
        {
            await FinishAuctionAsync(auctionId);
            return new AuctionBidAdditionResult(BidAdditionValidationCode.AuctionFinished);
        }

        if (user.Balance < bidValue) return new AuctionBidAdditionResult(BidAdditionValidationCode.LowBalance);
        if (auction.Bids.Count > 0)
        {
            var lastBidder = await _userReader.Read(auction.Bids[^1].Bidder.Id);
            await _userUpdater.UpdateBalance(lastBidder!.Id, lastBidder.Balance + auction.Bids[^1].Price);
            if (lastBidder.Id == user.Id) user.Balance += auction.Bids[^1].Price;
        }

        var bidAdditionResult = await _auctionUpdater.AddBid(auctionId, userId, bidValue);
        await _userUpdater.UpdateBalance(userId, user.Balance - bidValue);

        return bidAdditionResult;
    }

    public async Task<AuctionCreationResult> CreateAuctionAsync(AuctionCreationData creationData)
    {
        var creationResult = await _auctionCreator.Create(creationData);
        if (!creationResult.IsSuccessful) return creationResult;
        var auction = await _auctionReader.Read(creationResult.AuctionId!);
        RunningAuctions.AddOrUpdate(creationResult.AuctionId!, auction!, (_, _) => auction!);
        return creationResult;
    }

    public async Task FinishAuctionAsync(string auctionId)
    {
        var auction = await _auctionReader.Read(auctionId);
        if (auction is null || auction.Finish > DateTime.Now || auction.Winner is not null ||
            !RunningAuctions.ContainsKey(auctionId)) return;
        RunningAuctions.Remove(auctionId, out _);
        await _auctionUpdater.Finish(auctionId);
        auction = await _auctionReader.Read(auctionId); // needs to be updated after finish
        await AddAuctionActivityLogs(auction.Id);
        var owner = await _userReader.Read(auction!.Token.Owner.Id);
        await _userUpdater.UpdateBalance(owner!.Id, owner.Balance + auction.BestPrice);
        await _tokenUpdater.ChangeOwner(auction.Token.Id, auction.Winner!.Id);
    }

    private async Task AddAuctionActivityLogs(string auctionId)
    {
        var auction = await _auctionAccessor.GetById(auctionId);
        foreach (var bidder in auction.Bids
                     .Where(b => b.Bidder != auction.Token.Owner && b.Bidder != auction.Winner)
                     .Select(b => b.Bidder)
                     .Distinct())
            await _participationLogAccessor.Add(new AuctionParticipationLog(auction, auction.Token, bidder, false));
        await _finishLogAccessor.Add(new AuctionFinishLog(auction, auction.Winner, auction.Token, auction.Token.Owner,
            auction.Finish, auction.Bids.Max(b => b.Price)));
    }
}