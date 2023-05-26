namespace Contracts.FrontToBack.EntityCRUDData.EntityUpdateData;

public class ChangeBalanceData
{
    public string Id { get; set; }
    public string? Wallet { get; set; }
    public string? Seed { get; set; }
    public decimal Amount { get; set; }
}