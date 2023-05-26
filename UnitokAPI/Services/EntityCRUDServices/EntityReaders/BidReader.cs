using Contracts.BackToFront.Full.Entities;
using Domain.Accessors.Entities;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;

namespace Services.EntityCRUDServices.EntityReaders;

public class BidReader : IBidReader
{
    private readonly IDbBidAccessor _bidAccessor;
    private readonly IDtoCreator _dtoCreator;

    public BidReader(IDbBidAccessor bidAccessor, IDtoCreator dtoCreator)
    {
        _bidAccessor = bidAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<BidFull?> Read()
    {
        return _bidAccessor.GetAll().Select(b => _dtoCreator.CreateFull(b));
    }

    public async Task<BidFull?> Read(string id)
    {
        return _dtoCreator.CreateFull(await _bidAccessor.GetById(id));
    }
}