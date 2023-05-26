using Contracts.BackToFront.Full.Entities;

namespace UI.Models;

public class UserSearchResultVm
{
    public UserSearchResultVm(UserFull user)
    {
        ProfilePicUrl = user.ProfilePicUrl;
        FullName = user.FullName;
        Username = user.UserName!;
    }

    public string ProfilePicUrl { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
}