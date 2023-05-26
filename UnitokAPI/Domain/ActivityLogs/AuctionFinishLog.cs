using Domain.ActivityLogs.Enums;
using Domain.Entities;

namespace Domain.ActivityLogs;


public class AuctionFinishLog:ActivityLog
{
    public AuctionFinishLog(Auction auction, User winner, Token token, User seller, DateTime finishTime, decimal price):
        this()
    {
        Auction = auction;
        Winner = winner;
        Token = token;
        Seller = seller;
        FinishTime = finishTime;
        Price = price;
    }
    
    public AuctionFinishLog()
    {
        Code = ActivityCode.AuctionFinish;
    }

    public Auction Auction { get; set; }
    public User Winner { get; set; }
    public Token Token { get; set; }
    public User Seller { get; set; }
    public DateTime FinishTime { get; set; }
    public decimal Price { get; set; }
}