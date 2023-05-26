using Contracts.BackToFront.Full.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.EntityCRUDServices.EntityReaders;
using UI.Models;

namespace UI.Controllers;

[Route("authors")]
public class AuthorsController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUserReader _userReader;

    public AuthorsController(UserManager<User> userManager, IUserReader userReader)
    {
        _userManager = userManager;
        _userReader = userReader;
    }

    [HttpGet]
    public async Task<IActionResult> Authors(int? pageNumber, string sortOrder="nameAscending")
    {
        int pageSize = 10;
        var numb =  pageNumber is not null ? pageNumber.Value : 1;
        var totalPagesNumber = _userReader.Read().Count() / pageSize;
        ViewData["User"] = await _userManager.GetUserAsync(User);
        // var authors = _userReader.Read(user => user.CreatedTokens.Count > 0, user => user.UserName, parameters.PageNumber, parameters.PageSize).Select(u => new AuthorCardVM(u)).ToList();
        // return View(authors);
        ViewData["order"] = sortOrder;
        var authors = new List<AuthorCardVM>();
        switch (sortOrder)
        {
            case "tokens":
            {
                 authors = _userReader.Read(user => user.CreatedTokens.Count >= 0, user => user.CreatedTokens.Count, numb, 10).Select(u => new AuthorCardVM(u)).ToList();
                break;
            }
            case "balance":
            {
                 authors = _userReader.Read(user => user.CreatedTokens.Count >= 0, user => user.Balance, numb, 10).Select(u => new AuthorCardVM(u)).ToList();
                break;
            }
            case "nameAscending":
            {
                 authors = _userReader.Read(user => user.CreatedTokens.Count >= 0, user => user.UserName, numb, 10).Select(u => new AuthorCardVM(u)).ToList();
                break;
            }
            case "nameDescending":
            {
                 authors = _userReader.Read(user => user.CreatedTokens.Count >= 0, user => user.UserName, numb, 10, false).Select(u => new AuthorCardVM(u)).ToList();
                break;
            }
            default:
            {
                authors = _userReader.Read(user => user.CreatedTokens.Count >= 0, user => user.UserName, numb, 10).Select(u => new AuthorCardVM(u)).ToList();
                break;
            }
        }

        var page =  new PaginatedList<AuthorCardVM>(authors, authors.Count(), pageNumber ?? 1, pageSize);
        ViewBag.Items = page;
        return View(page);
    }
    
}