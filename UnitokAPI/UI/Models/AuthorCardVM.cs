using Contracts.BackToFront.Full.Entities;

namespace UI.Models;

public class AuthorCardVM
{
    public AuthorCardVM(UserFull? userFull)
    {
        Name = userFull.FullName;
        NickName = userFull.UserName;
        Description = userFull.Description;
    }

    public string Name { get; set; }
    public string NickName { get; set; }
    public string Description { get; set; }
}