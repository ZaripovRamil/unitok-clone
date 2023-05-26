using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Abstraction.Auctions;

namespace Services.Auctions;

public class AuctionBackgroundService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private Timer? _timer;

    public AuctionBackgroundService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    private async void DoWork(object? state)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var auctionService = scope.ServiceProvider.GetRequiredService<IAuctionService>();
            
        try
        {
            var activeAuctions = AuctionService.RunningAuctions;

            foreach (var (_, auction) in activeAuctions)
                if (DateTime.Now >= auction.Finish)
                    await auctionService.FinishAuctionAsync(auction.Id);
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync($"Error in {nameof(AuctionBackgroundService)}: {ex.Message}");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}