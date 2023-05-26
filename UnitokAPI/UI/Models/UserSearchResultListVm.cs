using Contracts.BackToFront.Full.Entities;

namespace UI.Models;

public class UserSearchResultListVm
{
    public UserSearchResultListVm(IEnumerable<UserFull> users)
    {
        Users = users.Select(u => new UserSearchResultVm(u)).ToList();
    }

    public List<UserSearchResultVm> Users { get; set; }
}