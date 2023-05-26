using Contracts.BackToFront.Full.Entities;
using Contracts.BackToFront.Light;
using Contracts.BackToFront.Light.Entities;
using Contracts.BackToFront.LogEntries;
using Domain.ActivityLogs;
using Domain.Entities;

namespace Services.Abstraction;

public interface IDtoCreator
{
    public AuctionFull? CreateFull(Auction? auction);
    public BidFull? CreateFull(Bid? bid);
    public TokenFull? CreateFull(Token? token);
    public UserFull? CreateFull(User? user);
    public AuthorizedUserFull? CreateAuthorized(User? user);
    public AuctionLight? CreateLight(Auction? auction);
    public BidLight? CreateLight(Bid? bid);
    public TokenLight? CreateLight(Token? token);
    public UserLight? CreateLight(User? user);
    public AuctionFinishLogDto Create(AuctionFinishLog log);
    public AuctionParticipanceLogDto Create(AuctionParticipationLog log);
    public TokenCreationLogDto Create(TokenCreationLog log);
}