using HKQTravelling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace HKQTravelling.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext data;

        public AccountController(ApplicationDBContext data)
        {
            this.data = data;
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
            string hashedPassword = GetMD5Hash(password);
            var dbUser = await data.users.FirstOrDefaultAsync(u => u.Username == username && u.Password == hashedPassword);
            if (dbUser != null)
            {
                if (dbUser.RoleId == 1)
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
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
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
            string birthdate = formCollection["BirthDate"].ToString();
            string niNumber = formCollection["NiNumber"].ToString();
            if (checkUsername(username))
            {
                ViewData["existed_user"] = "Tài khoản này đã tồn tại!";
                return View();
            }
            else if (checkEmail(email))
            {
                ViewData["existed_email"] = "Email này đã tồn tại";
                return View();
            }
            else
            {
                var dbUser = new Models.Users
                {
                    Username = username,
                    Password = GetMD5Hash(password),
                    Status = 1,
                    CreationDate = Convert.ToDateTime(DateTime.Today),
                    RoleId = 2 //Vai trò người dùng
                };

                var dbUserDetails = new Models.UserDetails
                {

                    Email = email,
                    PhoneNumber = phone_number,
                    Surname = surname,
                    Name = name,
                    Gender = gender == "Nam" ? 1 : 2,
                    Birthdate = Convert.ToDateTime(birthdate),
                    NiNumber = niNumber,
                    Age = caculateAge(Convert.ToDateTime(birthdate)),
                    users = dbUser
                };
                data.users.Add(dbUser);
                data.userDetails.Add(dbUserDetails);
                await data.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion

        #region functions
        // Hàm mã hóa MD5
        private string GetMD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                // Nếu input rỗng hoặc null, trả về chuỗi rỗng.
                return string.Empty;
            }
            using (var md5 = MD5.Create())
            {
                var dulieu = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();

                for (int i = 0; i < dulieu.Length; i++)
                {
                    builder.Append(dulieu[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private bool checkUsername(string username)
        {
            return data.users.Count(u => u.Username == username) > 0;
        }
        private bool checkEmail(string email)
        {
            return data.userDetails.Count(u => u.Email == email) > 0;
        }
        private int caculateAge(DateTime birthDay)
        {
            if(birthDay == null)
            {
                return 0;
            }
            else
            {
                DateTime today = DateTime.Today;
                int age = today.Year - birthDay.Year;

                if (birthDay > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        #endregion
    }
}
