using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class UsersController : Controller
{
    [HttpGet]
    public IActionResult Users()
    {
        return View();
    }
}