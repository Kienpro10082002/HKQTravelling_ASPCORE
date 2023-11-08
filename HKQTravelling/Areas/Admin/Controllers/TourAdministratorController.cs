using Microsoft.AspNetCore.Mvc;

namespace HKQTravelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("TourAdministrator")]
    public class TourAdministratorController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Show()
        {
            return View();
        }
    }
}
