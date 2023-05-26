using Domain.Entities;

namespace Domain.Accessors.Entities;

public interface IDbUserAccessor : IDbAccessor
{
    public Task<User?> GetById(string id);
    public Task<User?> GetByUsername(string name);
    public Task<User?> GetByEmail(string email);
    IEnumerable<User> GetAll();
    Task Update(User user);
}