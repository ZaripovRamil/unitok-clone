using Domain.ActivityLogs.Enums;
using Domain.Entities;

namespace Domain.ActivityLogs;

public class AuctionParticipationLog:ActivityLog
{
    public AuctionParticipationLog(Auction auction, Token token, User bidder, bool hasWon):this()
    {
        Auction = auction;
        Token = token;
        Bidder = bidder;
        HasWon = hasWon;
    }

    public AuctionParticipationLog()
    {
        Code = ActivityCode.AuctionParticipation;
    }

    public Auction Auction { get; set; }
    public Token Token { get; set; }
    public User Bidder { get; set; }
    public bool HasWon { get; set; }
}