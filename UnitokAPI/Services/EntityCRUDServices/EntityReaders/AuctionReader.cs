using Contracts.BackToFront.Full.Entities;
using Domain.Accessors.Entities;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;

namespace Services.EntityCRUDServices.EntityReaders;

public class AuctionReader : IAuctionReader
{
    private readonly IDbAuctionAccessor _auctionAccessor;
    private readonly IDtoCreator _dtoCreator;

    public AuctionReader(IDbAuctionAccessor auctionAccessor, IDtoCreator dtoCreator)
    {
        _auctionAccessor = auctionAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<AuctionFull?> Read()
    {
        return _auctionAccessor.GetAll().Select(a => _dtoCreator.CreateFull(a));
    }

    public async Task<AuctionFull?> Read(string id)
    {
        return _dtoCreator.CreateFull(await _auctionAccessor.GetById(id));
    }
}