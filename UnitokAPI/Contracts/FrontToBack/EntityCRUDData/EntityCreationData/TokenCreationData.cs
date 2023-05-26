using Domain.Entities.Enums;

namespace Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

public class TokenCreationData
{
    public string Name { get; set; }
    public TokenType Type { get; set; }
    public string CreatorId { get; set; }
    public string Description { get; set; }
}