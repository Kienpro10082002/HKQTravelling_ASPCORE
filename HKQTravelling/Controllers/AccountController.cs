using HKQTravelling.Extension;
using HKQTravelling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using X.PagedList;

namespace HKQTravelling.Controllers
{
    public class AccountController : Controller
    {
        #region Hung
        private readonly ApplicationDBContext data;
        public AccountController(ApplicationDBContext data)
        {
            this.data = data;

        }

        public IActionResult Index()
        {
            return RedirectToAction("Profile", "Account");
        }

        #region features
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection formCollection)
        {
            string username = formCollection["UserName"];
            string password = formCollection["Password"];
            string hashedPassword = Encrypt.GetMD5Hash(password);
            var dbUser = await data.users.FirstOrDefaultAsync(u => u.Username == username && u.Password == hashedPassword);
            if (dbUser != null)
            {
                ViewBag.thongbao = "đăng nhập thành công";
                if (dbUser.RoleId == 1) // Nếu là admin
                {
                    // Chuyển đổi đối tượng dbUser thành chuỗi JSON
                    var userJson = JsonConvert.SerializeObject(dbUser);

                    // Lưu chuỗi JSON vào phiên
                    HttpContext.Session.SetString("use", userJson);

                    // Lưu phiên lại
                    await HttpContext.Session.CommitAsync();
                    return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
                }
                else
                {
                    HttpContext.Session.Set("user_id", BitConverter.GetBytes(dbUser.UserId));
                    HttpContext.Session.SetString("user_account", JsonConvert.SerializeObject(dbUser));
                    var dbUserDetails = await data.userDetails.FirstOrDefaultAsync(u => u.users.UserId == dbUser.UserId);
                    HttpContext.Session.SetString("fullName", dbUserDetails.Surname + ' ' + dbUserDetails.Name);
                    HttpContext.Session.SetString("Email", dbUserDetails.Email);

                    return RedirectToAction("Index", "Tour");
                }
            }
            else
            {
                ViewData["LoginError"] = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormCollection formCollection)
        {
            string username = formCollection["UserName"].ToString();
            string password = formCollection["Password"].ToString();
            string email = formCollection["Email"].ToString();
            string phone_number = formCollection["PhoneNumber"].ToString();
            string surname = formCollection["Surname"].ToString();
            string name = formCollection["Name"].ToString();
            string gender = formCollection["Gender"].ToString();
            string birthdate = formCollection["Birthdate"].ToString();
            string niNumber = formCollection["NiNumber"].ToString();
            if (string.IsNullOrEmpty(username))
            {
                ViewData["checking_user"] = "Tài khoản không được để trống!";
                return View();
            }
            else if (CheckAccountInformation.checkUsername(data, username))
            {
                ViewData["checking_user"] = "Tài khoản này đã tồn tại!";
                return View();
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["checking_email"] = "Email không được để trống";
                return View();
            }
            else if (CheckAccountInformation.checkEmail(data, email))
            {
                ViewData["checking_email"] = "Email này đã tồn tại";
                return View();
            }
            else if (!CheckAccountInformation.IsEmailValid(email))
            {
                ViewData["checking_email"] = "Email không hợp lệ (có kí tự @ và sign)";
                return View();
            }
            else if (string.IsNullOrEmpty(phone_number))
            {
                ViewData["checking_phonenumber"] = "Số điện thoại trống";
                return View();
            }
            else if (!CheckAccountInformation.IsVietnamesePhoneNumber(phone_number))
            {
                ViewData["checking_phonenumber"] = "Số điện thoại không hợp lệ (SĐT VIỆT NAM)";
                return View();
            }
            else if (CheckAccountInformation.checkSurname(surname) == false)
            {
                ViewData["checking_surname"] = "Họ và tên lót trống";
                return View();
            }
            else if (CheckAccountInformation.checkName(name) == false)
            {
                ViewData["checking_name"] = "Tên trống";
                return View();
            }
            else if (string.IsNullOrEmpty(birthdate))
            {
                ViewData["checking_birthdate"] = "Vui lòng nhập ngày sinh của bạn.";
                return View();
            }
            else if (CheckAccountInformation.ValidateBirthdate(DateTime.Parse(birthdate)) == false)
            {
                ViewData["checking_birthdate"] = "Lưu ý tuổi không dưới 5 và trên 90 bạn ơi ";
                return View();
            }
            else
            {
                var dbUser = new Models.Users
                {
                    Username = username,
                    Password = Encrypt.GetMD5Hash(password),
                    Status = 1,
                    CreationDate = DateTime.Now,
                    RoleId = 2 // Vai trò người dùng
                };

                var dbUserDetails = new Models.UserDetails
                {
                    Email = email,
                    PhoneNumber = phone_number,
                    Surname = surname,
                    Name = name,
                    Gender = gender == "1" ? 1 : 2,
                    Birthdate = Convert.ToDateTime(birthdate),
                    NiNumber = niNumber,
                    Age = Caculation.caculateAge(Convert.ToDateTime(birthdate)),
                    users = dbUser
                };
                data.users.Add(dbUser);
                data.userDetails.Add(dbUserDetails);
                await data.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            // Kiểm tra xem khóa "user_id" có tồn tại trong Session không
            if (HttpContext.Session.TryGetValue("user_id", out byte[] userIdBytes))
            {

                // Nếu tồn tại, bạn có thể chuyển đổi giá trị từ mảng byte sang kiểu dữ liệu phù hợp
                long userId = BitConverter.ToInt64(userIdBytes, 0);
                var user = data.users.FirstOrDefault(x => x.UserId == userId);
                var dbUserDetails = await data.userDetails.FirstOrDefaultAsync(u => u.users.UserId == user.UserId);
                // Tiếp tục xử lý tại đây với userId

                return View(dbUserDetails);
            }
            else
            {
                // Nếu "user_id" không tồn tại trong Session, chuyển hướng đến trang đăng ký
                return RedirectToAction("Login", "Account");
            }
        }


        public IActionResult Logout()
        {
            // Xóa tất cả các thông tin liên quan đến phiên đăng nhập
            HttpContext.Session.Clear();


            return RedirectToAction("Index", "Tour");
        }
        #endregion



        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            // Kiểm tra xem khóa "user_id" có tồn tại trong Session không
            if (HttpContext.Session.TryGetValue("user_id", out byte[] userIdBytes))
            {

                // Nếu tồn tại, bạn có thể chuyển đổi giá trị từ mảng byte sang kiểu dữ liệu phù hợp
                long userId = BitConverter.ToInt64(userIdBytes, 0);
                var user = data.users.FirstOrDefault(x => x.UserId == userId);
                var dbUserDetails = await data.userDetails.FirstOrDefaultAsync(u => u.users.UserId == user.UserId);
                // Tiếp tục xử lý tại đây với userId

                return View(dbUserDetails);
            }
            else
            {
                // Nếu "user_id" không tồn tại trong Session, chuyển hướng đến trang đăng ký
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(FormCollection collection)
        {
            // Kiểm tra xem khóa "user_id" có tồn tại trong Session không
            if (HttpContext.Session.TryGetValue("user_id", out byte[] userIdBytes))
            {

                // Nếu tồn tại, bạn có thể chuyển đổi giá trị từ mảng byte sang kiểu dữ liệu phù hợp
                long userId = BitConverter.ToInt64(userIdBytes, 0);
                var user = data.users.FirstOrDefault(x => x.UserId == userId);

                // Tiếp tục xử lý tại đây với userId

                return View(user);
            }
            else
            {
                // Nếu "user_id" không tồn tại trong Session, chuyển hướng đến trang đăng ký
                return RedirectToAction("Login", "Account");
            }




        }




        /* //Get Session["user_account"] are logged in before
         var userAccount = (user_account)Session["user_account"];

         //Get collection from changePassword.html with the attribute is name
         var pass = mahoamd5(collection["current_password"]);
         var new_pass = mahoamd5(collection["new_password"]);
         var confirm_pass = mahoamd5(collection["confirm_password"]);

         //create variable and check info
         var check_user = data.user_accounts.SingleOrDefault(model => model.user_name == userAccount.user_name && model.user_password == userAccount.user_password);
         var check_password = data.user_accounts.SingleOrDefault(model => model.user_password == pass);

         if (check_user == null)
         {
             ViewData["WrongUser"] = "Vui lòng nhập đúng thông tin";
         }
         else if (check_password == null)
         {
             ViewData["WrongPassword"] = "Vui lòng nhập đúng mật khẩu";
         }
         else if (confirm_pass != new_pass)
         {
             ViewData["WrongNewPassword"] = "Vui lòng nhập khớp mật khẩu mới";
         }
         else //successful
         {
             check_user.user_password = confirm_pass;
         }
         data.SubmitChanges();
         return RedirectToAction("Index", "Home");
     }*/
        #endregion


        #region Tour Booked

        [HttpGet]
        public IActionResult TourBooked(int? page)
        {
            int pageSize = 100000;
            int pageNumber = (page ?? 1);
            var userId = HttpContext.Session.Get("user_id");
            var dbUserId = BitConverter.ToInt64(userId);
            IPagedList<Bookings> objTourBookedList = data.bookings.Where(x => x.UserId == dbUserId).ToPagedList(pageNumber, pageSize);
            var user = data.users.ToList();
            var userDetail = data.userDetails.ToList();
            var tour = data.tours.ToList();
            var tourImages = data.tourImages.ToList();

            var tourImageUrls = new List<string>();
            foreach (var tours in objTourBookedList)
            {
                var image = tourImages.FirstOrDefault(ti => ti.TourId == tours.TourId);
                if (image != null)
                {
                    tourImageUrls.Add(image.ImageUrl);
                }
            }
            ViewBag.TourImages = tourImageUrls;
            ViewBag.Tour = tour;
            return View(objTourBookedList);
        }

        #endregion
    }
}
