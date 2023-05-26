using Domain.Entities;

namespace Domain.Accessors.Entities;

public interface IDbTokenAccessor : IDbAccessor
{
    public Task Add(Token token);
    public Task<Token?> GetById(string id);
    public Task Delete(Token token);
    IEnumerable<Token> GetAll();
    public Task UpdateFileId(string tokenId, string fileId);
}