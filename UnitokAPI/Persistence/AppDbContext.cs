using Domain.ActivityLogs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Token> Tokens { get; set; }
    public DbSet<Auction> Auctions { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<TokenCreationLog> TokenCreationLogs { get; set; }
    public DbSet<AuctionFinishLog> AuctionFinishLogs { get; set; }
    public DbSet<AuctionParticipationLog> AuctionParticipationLogs { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql("Server=localhost;Database=Unitok;Port=5432;username = postgres;SSLMode=Prefer");
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = "defaultuser", Email = "", UserName = "rzaripov", FirstName = "Ramil", LastName = "Zaripov",
            Description = "bread sphere hung itself"
        });
        modelBuilder.Entity<Token>()
            .HasOne(t => t.TokenCreationLog)
            .WithOne(tl => tl.Token)
            .HasForeignKey<TokenCreationLog>(tl => tl.TokenId);
    }
}