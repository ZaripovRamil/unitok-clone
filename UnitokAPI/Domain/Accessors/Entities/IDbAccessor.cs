namespace Domain.Accessors.Entities;

public interface IDbAccessor
{
    public Task SaveChangesAsync();
}