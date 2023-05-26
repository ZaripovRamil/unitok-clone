using Contracts.BackToFront.EntityCRUDResults.EntityUpdateResults;
using Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;

namespace Services.Abstraction.EntityCRUDServices.EntityUpdaters;

public interface IUserUpdater
{
    public Task<UserBalanceUpdateResult> UpdateBalance(string userId, decimal newBalance);
    public Task<UserUpdateResult> Update(ChangeBalanceData data);
    public Task<UserUpdateResult> Update(ChangePasswordData data);
    public Task<UserUpdateResult> Update(ChangeProfileDetailsData data);
}