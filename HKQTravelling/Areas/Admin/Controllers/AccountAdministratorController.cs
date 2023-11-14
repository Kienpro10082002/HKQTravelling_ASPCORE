using HKQTravelling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace HKQTravelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("AccountAdministrator")]
    public class AccountAdministratorController : Controller
    {
        private readonly ApplicationDBContext data;
        public AccountAdministratorController(ApplicationDBContext data)
        {
            this.data = data;

        }

        [HttpGet]
        [Route("IndexForAdmin")]
        public async Task<IActionResult> IndexForAdmin()
        {
            var allUsers = await data.users.ToListAsync();
            var admins =  allUsers.Where(u => u.RoleId == 1).ToList();
            return View(admins);
        }

        [HttpGet]
        [Route("IndexForUser")]
        public async Task<IActionResult> IndexForUser()
        {
            var allUsers = await data.users.ToListAsync();
            var users = allUsers.Where(u => u.RoleId == 2).ToList();
            return View(users);
        }

        [HttpGet]
        [Route("CreateForAdmin")]
        public IActionResult CreateForAdmin()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateForAdmin")]
        public IActionResult CreateForAdmin(ICollection collection)
        {
            // Logic để xử lý HTTP POST và tạo mới người dùng cho admin
            // Sử dụng model để lấy dữ liệu từ form
            // Thêm logic xử lý tạo mới người dùng ở đây

            // Sau khi tạo mới, có thể chuyển hướng hoặc trả về view tùy thuộc vào kết quả
            return RedirectToAction("IndexForAdmin");
        }

    }
}
