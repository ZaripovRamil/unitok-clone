using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Search;
using UI.Models;

namespace UI.Controllers;

[Route("Search")]
public class SearchController : Controller
{
    private readonly ISearchEngine _searchEngine;

    public SearchController(ISearchEngine searchEngine)
    {
        _searchEngine = searchEngine;
    }

    [HttpGet("Users")]
    public IActionResult SearchUsers([FromQuery] string query)
    {
        var vm = new UserSearchResultListVm(_searchEngine.SearchUsers(query).Take(10));
        return PartialView("_UserSearchResultListPartial", vm);
    }
    
    [HttpGet("Tokens")]
    public IActionResult SearchTokens([FromQuery] string query)
    {
        var vm = new TokenSearchResultListVm(_searchEngine.SearchTokens(query).Take(10));
        return PartialView("_TokenSearchResultListPartial", vm);
    }

    [HttpGet("TokensAndUsers")]
    public IActionResult Search([FromQuery] string query)
    {
        var vm = new EntitySearchResultVm(_searchEngine.Search(query));
        return PartialView("_SearchResultListPartial", vm);
    }
   
}