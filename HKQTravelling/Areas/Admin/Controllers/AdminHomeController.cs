using HKQTravelling.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HKQTravelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("AdminHome")]
    public class AdminHomeController : Controller
    {
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            // Lấy chuỗi JSON từ phiên
            var userJson = HttpContext.Session.GetString("use");

            if (!string.IsNullOrEmpty(userJson))
            {
                // Chuyển đổi chuỗi JSON thành đối tượng dbUser
                var dbUser = JsonConvert.DeserializeObject<Users>(userJson);

                // Sử dụng dbUser
                if (dbUser.RoleId == 1)
                {
                    return View();
                }
                else
                {
                    return RedirectPermanent("~/Home/Index");
                }
            }
            return RedirectPermanent("~/Home/Index");
        }
    }
}
