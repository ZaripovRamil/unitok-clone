using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class NotificationsController : Controller
{
    [HttpGet]
    public IActionResult Notifications()
    {
        return View();
    }
}