using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class AuctionsController : Controller
{
    [HttpGet]
    public IActionResult Auctions()
    {
        return View();
    }
}