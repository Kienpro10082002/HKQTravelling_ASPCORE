﻿using HKQTravelling.Extension;
using HKQTravelling.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
            string hashedPassword = Encrypt.GetMD5Hash(password);
            var dbUser = await data.users.FirstOrDefaultAsync(u => u.Username == username && u.Password == hashedPassword);
            var dbUserDetails = await data.userDetails.FirstOrDefaultAsync(u => u.UserDetailId == u.UserDetailId); 
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
                    HttpContext.Session.SetString("user_account", JsonConvert.SerializeObject(dbUser));
                    HttpContext.Session.SetString("fullName", dbUserDetails.Surname +' '+ dbUserDetails.Name);
                    HttpContext.Session.SetString("Email", dbUserDetails.Email);
                    HttpContext.Session.Set("user_id", BitConverter.GetBytes(dbUser.UserId));

                    return RedirectToAction("Index", "Home");
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
            else if (CheckAccountInformation.ValidateBirthdate(DateTime.Parse(birthdate))==false)
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
                    Gender = gender == "Nam" ? 1 : 2,
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
        public async Task<IActionResult> Logout()
        {
            // Xóa tất cả các thông tin liên quan đến phiên đăng nhập
            HttpContext.Session.Clear();

          
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
