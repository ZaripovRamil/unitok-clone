using Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace Contracts.FrontToBack.EntityCRUDData.EntityCreationData;

public class TokenCreationDataWithFile: TokenCreationData
{
    public IFormFile File { get; set; }
}