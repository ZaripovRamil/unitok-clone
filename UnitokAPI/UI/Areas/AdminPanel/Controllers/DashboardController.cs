using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Dashboard()
    {
        return View();
    }
}