using Contracts.BackToFront.Full.Entities;
using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;
using Contracts.BackToFront.LogEntries;
using Domain.ActivityLogs;
using Domain.Entities;
using Services.Abstraction;

namespace Services;

public class DtoCreator : IDtoCreator
{
    public AuctionFull? CreateFull(Auction? auction)
    {
        return auction == null
            ? null
            : new AuctionFull
            {
                Id = auction.Id,
                Bids = auction.Bids.Select(CreateLight).ToList()!,
                BestPrice = auction.BestPrice,
                Finish = auction.Finish,
                Start = auction.Start,
                StartPrice = auction.StartPrice,
                Token = CreateLight(auction.Token)!,
                Winner = CreateLight(auction.Winner)
            };
    }


    public BidFull? CreateFull(Bid? bid)
    {
        return bid == null
            ? null
            : new BidFull
            {
                Id = bid.Id,
                Auction = CreateLight(bid.Auction)!,
                Bidder = CreateLight(bid.Bidder)!,
                BidTime = bid.BidTime,
                Price = bid.Price
            };
    }

    public TokenFull? CreateFull(Token? token)
    {
        return token == null
            ? null
            : new TokenFull
            {
                Author = CreateLight(token.Author)!,
                CreatedAt = token.CreatedAt,
                Description = token.Description,
                FileId = token.FileId!,
                Id = token.Id,
                Name = token.Name,
                Owner = CreateLight(token.Owner)!,
                Type = (int)token.Type
            };
    }

    public UserFull? CreateFull(User? user)
    {
        return user == null
            ? null
            : new UserFull
            {
                Id = user.Id,
                CreatedTokens = user.CreatedTokens.Select(CreateLight).ToList()!,
                OwnedTokens = user.OwnedTokens.Select(CreateLight).ToList()!,
                SellLogs = user.SellLogs.Select(Create).ToList()!,
                BuyLogs = user.BuyLogs.Select(Create).ToList(),
                ParticipanceLogs = user.ParticipationLogs.Select(Create).ToList(),
                TokenCreationLogs = user.TokenCreationLogs.Select(Create).ToList(),
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                Description = user.Description,
                ProfilePicUrl = user.ProfilePicUrl,
                UserName = user.UserName,
                Wallet = user.Wallet!,
                Balance = user.Balance,
            };
    }

    public AuthorizedUserFull? CreateAuthorized(User? user)
    {
        return user == null
            ? null
            : new AuthorizedUserFull
            {
                Email = user.Email!,
                Balance = user.Balance,
                CreatedTokens = user.CreatedTokens.Select(CreateLight).ToList()!,
                OwnedTokens = user.OwnedTokens.Select(CreateLight).ToList()!,
                SellLogs = user.SellLogs.Select(Create).ToList()!,
                BuyLogs = user.BuyLogs.Select(Create).ToList(),
                ParticipanceLogs = user.ParticipationLogs.Select(Create).ToList(),
                TokenCreationLogs = user.TokenCreationLogs.Select(Create).ToList(),
                Description = user.Description,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                ProfilePicUrl = user.ProfilePicUrl,
                UserName = user.UserName,
                Seed = user.Seed!,
                Wallet = user.Wallet!,
            };
    }

    public BidLight? CreateLight(Bid? bid)
    {
        return bid == null
            ? null
            : new BidLight
            {
                BidTime = bid.BidTime,
                Bidder = CreateLight(bid.Bidder)!,
                Id = bid.Id,
                Price = bid.Price
            };
    }

    public UserLight? CreateLight(User? user)
    {
        return user == null
            ? null
            : new UserLight
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfilePicUrl = user.ProfilePicUrl
            };
    }

    public AuctionFinishLogDto Create(AuctionFinishLog log)
    {
        return new AuctionFinishLogDto
        {
            Id = log.Id,
            Auction = CreateLight(log.Auction)!,
            Winner = CreateLight(log.Winner)!,
            Seller = CreateLight(log.Seller)!,
            Code = log.Code,
            FinishTime = log.FinishTime,
            Price = log.Price,
            Token = CreateLight(log.Token)!
        };
    }

    public AuctionParticipanceLogDto Create(AuctionParticipationLog log)
    {
        return new AuctionParticipanceLogDto
        {
            Auction = CreateLight(log.Auction)!,
            Code = log.Code,
            Id = log.Id,
            HasWon = log.HasWon,
            Token = CreateLight(log.Token)!,
            Bidder = CreateLight(log.Bidder)!
        };
    }

    public TokenCreationLogDto Create(TokenCreationLog log)
    {
        return new TokenCreationLogDto
        {
            Id = log.Id,
            Code = log.Code,
            CreationTime = log.CreationTime,
            Token = CreateLight(log.Token)!,
            Author = CreateLight(log.Author)!
        };
    }

    public TokenLight? CreateLight(Token? token)
    {
        return token == null
            ? null
            : new TokenLight
            {
                Author = CreateLight(token.Author)!,
                FileId = token.FileId!,
                Id = token.Id,
                Name = token.Name,
                Type = (int)token.Type,
                Owner = CreateLight(token.Owner)!
            };
    }

    public AuctionLight? CreateLight(Auction? auction)
    {
        return auction == null
            ? null
            : new AuctionLight
            {
                Id = auction.Id,
                Finish = auction.Finish,
                Price = auction.BestPrice,
                Start = auction.Start,
                Token = CreateLight(auction.Token)!
            };
    }
}