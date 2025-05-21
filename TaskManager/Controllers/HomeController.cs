using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Controllers;

// HomeController serves the landing page for the application.
public class HomeController : Controller
{
    // GET: /Home/Index
    public IActionResult Index()
    {
        // Redirect to Tasks if authenticated, else to login
        if (User.Identity != null && User.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Tasks");
        return RedirectToAction("Login", "Account");
    }
}
