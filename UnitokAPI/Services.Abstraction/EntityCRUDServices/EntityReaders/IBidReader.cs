using Contracts.BackToFront.Full.Entities;

namespace Services.Abstraction.EntityCRUDServices.EntityReaders;

public interface IBidReader
{
    public IEnumerable<BidFull?> Read();
    public Task<BidFull?> Read(string id);
}