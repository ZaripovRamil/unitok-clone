using Contracts.BackToFront.Full.Entities;

namespace Services.Abstraction.EntityCRUDServices.EntityReaders;

public interface IAuctionReader
{
    public IEnumerable<AuctionFull?> Read();
    public Task<AuctionFull?> Read(string id);
}