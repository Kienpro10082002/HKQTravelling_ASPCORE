using HKQTravelling.Areas.Admin.Extension;
using HKQTravelling.Areas.Admin.Models;
using HKQTravelling.Extension;
using HKQTravelling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HKQTravelling.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("AccountAdministrator")]
    public class AccountAdministratorController : Controller
    {
        private readonly ApplicationDBContext _context;
        public AccountAdministratorController(ApplicationDBContext _context)
        {
            this._context = _context;

        }

        #region Admin administrator

        [HttpGet]
        [Route("IndexForAdmin")]
        public async Task<IActionResult> IndexForAdmin()
        {
            var allUsers = await _context.users.ToListAsync();
            var admins = allUsers.Where(u => u.RoleId == 1).ToList();
            return View(admins);
        }

        // GET: Admin/AccountAdministrator/DetailsForAdmin/5
        [HttpGet]
        [Route("DetailsForAdmin/{id}")]
        public async Task<IActionResult> DetailsForAdmin(long? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var users = await _context.userDetails
                .Include(u => u.users)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> CreateForAdmin(IFormCollection formCollection)
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
            else if (CheckAccountInformation.checkUsername(_context, username))
            {
                ViewData["checking_user"] = "Tài khoản này đã tồn tại!";
                return View();
            }
            else if (string.IsNullOrEmpty(email))
            {
                ViewData["checking_email"] = "Email không được để trống";
                return View();
            }
            else if (CheckAccountInformation.checkEmail(_context, email))
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
            else if (CheckAccountInformation.ValidateBirthdate(DateTime.Parse(birthdate)) == false)
            {
                ViewData["checking_birthdate"] = "Lưu ý tuổi không dưới 5 và trên 90 bạn ơi ";
                return View();
            }
            else
            {
                var dbUser = new Users
                {
                    Username = username,
                    Password = Encrypt.GetMD5Hash(password),
                    Status = 1,
                    CreationDate = DateTime.Now,
                    RoleId = 1 // Vai trò người quản trị
                };

                var dbUserDetails = new UserDetails
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
                _context.users.Add(dbUser);
                _context.userDetails.Add(dbUserDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexForAdmin");
            }
        }

        // GET: Admin/AccountAdministrator/EditForAdmin/5
        [HttpGet]
        [Route("EditForAdmin/{id}")]
        public async Task<IActionResult> EditForAdmin(long? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            UserDetails userDetails = await _context.userDetails.FirstOrDefaultAsync(u => u.UserId == users.UserId);
            //if(userDetails == null)
            //{
            //    return NotFound();
            //}

            var userAndDetailsViewModel = new UserAndDetailsViewModel()
            {
                User = users,
                UserDetails = userDetails
            };
            return View(userAndDetailsViewModel);
        }

        // POST: Admin/AccountAdministrator/EditForAdmin/5
        [HttpPost]
        [Route("EditForAdmin/{id}")]
        public async Task<IActionResult> EditForAdmin(long id, IFormCollection formCollection)
        {
            string password = formCollection["Password"].ToString();
            string email = formCollection["Email"].ToString();
            string phone_number = formCollection["PhoneNumber"].ToString();
            string surname = formCollection["Surname"].ToString();
            string name = formCollection["Name"].ToString();
            string gender = formCollection["Gender"].ToString();
            string dateString = formCollection["Birthdate"].ToString();
            DateTime? birthdate = null;

            if (!string.IsNullOrEmpty(dateString) && DateTime.TryParse(dateString, out DateTime result))
            {
                birthdate = result;
            }
            string niNumber = formCollection["NiNumber"].ToString();
            var dbUsers = await _context.userDetails
                .Include(u => u.users)
                .FirstOrDefaultAsync(u => u.UserId == id);
            try
            {
                if (CheckAccountInformation.checkEmail(_context, email))
                {
                    ViewData["checking_email"] = "Email này đã tồn tại";
                    return View();
                }
                else if (!CheckAccountInformation.IsEmailValid(email))
                {
                    ViewData["checking_email"] = "Email không hợp lệ (có kí tự @ và sign)";
                    return View();
                }
                else if (!CheckAccountInformation.IsVietnamesePhoneNumber(phone_number))
                {
                    ViewData["checking_phonenumber"] = "Số điện thoại không hợp lệ (SĐT VIỆT NAM)";
                    return View();
                }
                else
                {
                    if(dbUsers.users.Password != password)
                    {
                        dbUsers.users.Password = Encrypt.GetMD5Hash(password);
                    }
                    dbUsers.users.UpdateDate = DateTime.Now;
                    if (dbUsers.Email != email)
                    {
                        dbUsers.Email = email;
                    }
                    if (dbUsers.PhoneNumber != phone_number)
                    {
                        dbUsers.PhoneNumber = phone_number;
                    }
                    if (dbUsers.Surname != surname)
                    {
                        dbUsers.Surname = surname;
                    }
                    if (dbUsers.Name != name)
                    {
                        dbUsers.Name = name;
                    }
                    dbUsers.Gender = gender == "Nam" ? 1 : 2;
                    dbUsers.Birthdate = Convert.ToDateTime(birthdate);
                    if (dbUsers.NiNumber != niNumber)
                    {
                        dbUsers.NiNumber = niNumber;
                    }
                    dbUsers.Age = Caculation.caculateAge(Convert.ToDateTime(birthdate));

                    _context.Update(dbUsers);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("IndexForAdmin");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                //Điều hướng đến trang lỗi
                return View();
            }
        }

        // GET: Admin/AccountAdministrator/Delete/5
        [HttpGet]
        [Route("DeleteForAdmin/{id}")]
        public async Task<IActionResult> DeleteForAdmin(long? id)
        {
            if (id == null || _context.users == null)
            {
                return NotFound();
            }

            var users = await _context.users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Admin/AccountAdministrator/Delete/5
        [HttpPost]
        [Route("DeleteForAdmin/{id}")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.users'  is null.");
            }
            var userDetails = await _context.userDetails.Include(u => u.users).FirstOrDefaultAsync(u => u.UserId == id);
            if (userDetails != null)
            {
                _context.userDetails.Remove(userDetails);
                var users = await _context.users.FirstOrDefaultAsync(u => u.UserId == id);
                _context.users.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexForAdmin));
        }

        private bool UsersExists(long id)
        {
            return (_context.users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        #endregion

        #region User administrator

        [HttpGet]
        [Route("IndexForUser")]
        public async Task<IActionResult> IndexForUser()
        {
            var allUsers = await _context.users.ToListAsync();
            var users = allUsers.Where(u => u.RoleId == 2).ToList();
            return View(users);
        }

        #endregion
    }
}
