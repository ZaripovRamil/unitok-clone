using Contracts.BackToFront.Full.Entities;
using Domain.Accessors.Entities;
using Domain.Entities;
using Services.Abstraction;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using Services.Extensions;

namespace Services.EntityCRUDServices.EntityReaders;

public class UserReader : IUserReader
{
    private readonly IDbUserAccessor _userAccessor;
    private readonly IDtoCreator _dtoCreator;

    public UserReader(IDbUserAccessor userAccessor, IDtoCreator dtoCreator)
    {
        _userAccessor = userAccessor;
        _dtoCreator = dtoCreator;
    }

    public IEnumerable<UserFull?> Read()
    {
        return _userAccessor.GetAll().Select(u => _dtoCreator.CreateFull(u));
    }

    public IEnumerable<UserFull?> Read(Predicate<User> filter, Func<User, IComparable> orderBy, int pageNumber,
        int pageSize, bool orderByAscending = true)
    {
        return _userAccessor.GetAll()
            .Where(u => filter(u))
            .FlexibleOrderBy(orderBy, orderByAscending)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Select(u => _dtoCreator.CreateFull(u));
    }

    public async Task<UserFull?> Read(string id)
    {
        return _dtoCreator.CreateFull(await _userAccessor.GetById(id));
    }

    public async Task<UserFull?> ReadByUsername(string username)
    {
        return _dtoCreator.CreateFull(await _userAccessor.GetByUsername(username));
    }

    public async Task<AuthorizedUserFull?> ReadAuthorizedByUsername(string username)
    {
        return _dtoCreator.CreateAuthorized(await _userAccessor.GetByUsername(username));
    }
}