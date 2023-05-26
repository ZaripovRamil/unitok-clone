using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class NFTsController : Controller
{
    [HttpGet]
    public IActionResult NFTs()
    {
        return View();
    }
}