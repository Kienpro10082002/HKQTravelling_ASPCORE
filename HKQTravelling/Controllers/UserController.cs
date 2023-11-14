using Microsoft.AspNetCore.Mvc;

namespace HKQTravelling.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
