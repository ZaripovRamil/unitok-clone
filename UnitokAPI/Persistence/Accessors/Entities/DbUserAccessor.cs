using Domain.Accessors.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Accessors.Entities;

public class DbUserAccessor : DbAccessor, IDbUserAccessor
{
    public DbUserAccessor(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetById(string id) =>
        await DbContext.Users
            .Include(u => u.TokenCreationLogs)
            .Include(u => u.SellLogs)
            .Include(u => u.BuyLogs)
            .Include(u => u.ParticipationLogs)
            .Include(u => u.OwnedTokens)
            .Include(u => u.CreatedTokens)
            .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByUsername(string username) =>
        await DbContext.Users
            .Include(u => u.TokenCreationLogs)
            .Include(u => u.SellLogs)
            .Include(u => u.BuyLogs)
            .Include(u => u.ParticipationLogs)
            .Include(u => u.OwnedTokens)
                .ThenInclude(t => t.Author)
            .Include(u => u.CreatedTokens)
            .FirstOrDefaultAsync(u => u.UserName == username);

    public async Task<User?> GetByEmail(string email) =>
        await DbContext.Users
            .Include(u => u.TokenCreationLogs)
            .Include(u => u.SellLogs)
            .Include(u => u.BuyLogs)
            .Include(u => u.ParticipationLogs)
            .Include(u => u.OwnedTokens)
            .Include(u => u.CreatedTokens)
            .FirstOrDefaultAsync(u => u.Email == email);

    public IEnumerable<User> GetAll()
    {
        return DbContext.Users
            .Include(u => u.TokenCreationLogs)
            .Include(u => u.SellLogs)
            .Include(u => u.BuyLogs)
            .Include(u => u.ParticipationLogs)
            .Include(u => u.OwnedTokens)
            .Include(u => u.CreatedTokens);
    }

    public async Task Update(User user)
    {
        DbContext.Users.Update(user);
        await DbContext.SaveChangesAsync();
    }
        
    
}