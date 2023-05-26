using Contracts.BackToFront.Full.Entities;
using Domain.Entities;

namespace Services.Abstraction.EntityCRUDServices.EntityReaders;

public interface IUserReader
{
    public IEnumerable<UserFull?> Read();

    public IEnumerable<UserFull?> Read(Predicate<User> filter, Func<User, IComparable> orderBy, int pageNumber,
        int pageSize, bool orderByAscending = true);

    public Task<UserFull?> Read(string id);
    public Task<UserFull?> ReadByUsername(string username);
    public Task<AuthorizedUserFull?> ReadAuthorizedByUsername(string username);
}