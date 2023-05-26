using Domain.Accessors.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.Entities;

public class DbTokenAccessor : DbAccessor, IDbTokenAccessor
{
    public async Task Add(Token token)
    {
        await DbContext.Tokens.AddAsync(token);
        await DbContext.SaveChangesAsync();
    }

    public async Task<Token?> GetById(string id) =>
        await DbContext.Tokens
            .Include(t => t.TokenCreationLog)
            .Include(t => t.TokenAuctions)
            .Include(t => t.Author)
            .Include(t => t.Owner)
            .FirstOrDefaultAsync(t => t.Id == id);

    public DbTokenAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Delete(Token token)
    {
        DbContext.Tokens.Remove(token);
        await DbContext.SaveChangesAsync();
    }

    public IEnumerable<Token> GetAll()
    {
        return DbContext.Tokens
            .Include(t => t.TokenCreationLog)
            .Include(t => t.TokenAuctions)
            .Include(t => t.Author)
            .Include(t => t.Owner);
    }

    public async Task UpdateFileId(string tokenId, string fileId)
    {
        var token = DbContext.Tokens.FirstOrDefault(t => t.Id == tokenId);
        token.FileId = fileId;
        await DbContext.SaveChangesAsync();
    }
}