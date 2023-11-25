//using database
#region Khai báo
using HKQTravelling.Extension;
using HKQTravelling.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using MailKit.Net.Smtp;
using NETCore.MailKit;
using MimeKit;
using X.PagedList;
using System.Linq;
using System.Collections.Generic;

#endregion


namespace HKQTravelling.Controllers
{
    public class TourController : Controller
    {
        #region Recourse
        //khai báo hàm app 
        private readonly ApplicationDBContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TourController(ApplicationDBContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            IPagedList<Tours> objTourList = _db.tours.ToPagedList(pageNumber, pageSize);
            var startLocations = _db.startLocations.ToList();
            var endLocations = _db.endLocations.ToList();
            var tourImages = _db.tourImages.ToList();

            var tourImageUrls = new List<string>();
            foreach (var tour in objTourList)
            {
                var image = tourImages.FirstOrDefault(ti => ti.TourId == tour.TourId);
                if (image != null)
                {
                    tourImageUrls.Add(image.ImageUrl);
                }
            }

            ViewBag.StartLocations = new SelectList(startLocations, "StartLocationId", "StartLocationName");
            ViewBag.EndLocations = new SelectList(endLocations, "EndLocationId", "EndLocationName");
            ViewBag.TourImages = tourImageUrls;
            return View(objTourList);
        }
        #endregion

        #region DetailTour
        [HttpGet]

        public IActionResult DetailTour(long id, IFormCollection f)
        {
            var soluongTreEm = f["soluongTreEm"].ToString();
            var soluongNguoiLon = f["soluongNguoiLon"].ToString();
            var tourImages = _db.tourImages.Where(t => t.TourId == id).ToList();
            var detail = _db.tours.FirstOrDefault(t => t.TourId == id);
            var tourDays = _db.tourDays.Where(t => t.TourId == id).OrderBy(t => t.DayNumber).ToList();
            if (tourImages == null && detail == null)
            {
                return NotFound();
            }
            var imageUrls = tourImages.Select(t => t.ImageUrl).ToList();


            ViewBag.Detail = detail;
            ViewBag.ImageUrls = imageUrls;
            ViewBag.TourDays = tourDays;

            return View();
        }
        #endregion

        #region BookingTour
        [HttpPost]
        public async Task<IActionResult> DetailTour(IFormCollection f)
        {
            var tourId = f["tourId"].ToString();
            var numAdults = f["nguoilon"].ToString();
            var numKids = f["treem"].ToString();
            var price = f["tongcong"].ToString();
            var roundedPrice = Math.Round(double.Parse(price));
            var userId = HttpContext.Session.Get("user_id");
            var dbUserId = BitConverter.ToInt64(userId);
            var dbtourId = Convert.ToInt32(tourId);
            /*            var dbbookingId = Convert.ToInt32(tourId);*/
            // Lưu tourId và userId vào phiên
            HttpContext.Session.SetInt32("tourId", dbtourId);
            /*            HttpContext.Session.SetInt32("bookingId", dbbookingId);*/
            if (string.IsNullOrEmpty(numAdults))
            {
                ViewData["checking_numAdults"] = "Chưa chọn số người đi!";
                return View();
            }
            else if (string.IsNullOrEmpty(numAdults))
            {
                ViewData["checking_numKids"] = "Chưa chọn số trẻ em!";
                return View();
            }
            else
            {
                var booking = new Models.Bookings
                {
                    BookingDate = DateTime.Now,
                    TourId = long.Parse(tourId),
                    NumAdults = int.Parse(numAdults),
                    NumKids = int.Parse(numKids),
                    PriceAdults = roundedPrice,
                    PriceKids = null,
                    PriceToddlers = null,
                    UserId = dbUserId,
                    NumToddlers = null
                };
                _db.bookings.Add(booking);
                await _db.SaveChangesAsync();
                var dbbookingId = Convert.ToInt32(booking.BookingId);
                HttpContext.Session.SetInt32("bookingId", dbbookingId);
                return RedirectToAction("Payments", "Tour");
            }
        }

        #endregion

        #region Add Tour
        [HttpGet]
        public IActionResult AddTour()
        {
            ViewBag.StartLocationName = new SelectList(_db.startLocations.ToList().OrderBy(n => n.StartLocationName), "StartLocationId", "StartLocationName");
            ViewBag.EndLocationName = new SelectList(_db.endLocations.ToList().OrderBy(n => n.EndLocationName), "EndLocationId", "EndLocationName");
            ViewBag.DisName = new SelectList(_db.discounts.ToList().OrderBy(n => n.DiscountId), "DiscountId", "DiscountName");
            ViewBag.StartLocationId = new SelectList(_db.startLocations.ToList().OrderBy(n => n.StartLocationName), "StartLocationId", "StartLocationId");
            ViewBag.EndLocationId = new SelectList(_db.endLocations.ToList().OrderBy(n => n.EndLocationName), "EndLocationId", "EndLocationId");
            ViewBag.DisId = new SelectList(_db.discounts.ToList().OrderBy(n => n.DiscountId), "DiscountId", "DiscountId");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTour(Tours tours, IFormCollection f)
        {
            var tourName = f["TourName"].ToString();
            var price = f["Price"].ToString();
            var startDate = f["StartDate"].ToString();
            var endDate = f["EndDate"].ToString();
            var status = f["Status"].ToString();
            var updateDate = f["UpdateDate"].ToString();
            var remaining = f["Remaining"].ToString();
            var disId = Request.Form["DiscountId"].ToString().Split(',')[0];
            var startLocationId = Request.Form["StartLocationId"].ToString().Split(',')[0];
            var endLocationId = Request.Form["EndLocationId"].ToString().Split(',')[0];


            if (string.IsNullOrEmpty(tourName))
            {
                ViewData["checking_tourName"] = "TourName trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(price))
            {
                ViewData["checking_price"] = "Price trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(startDate))
            {
                ViewData["checking_startDate"] = "StartDate trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(endDate))
            {
                ViewData["checking_endDate"] = "EndDate trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(updateDate))
            {
                ViewData["checking_UpdateDate"] = "UpdateDate trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(remaining))
            {
                ViewData["checking_remaining"] = "Remaining trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(disId))
            {
                ViewData["checking_DisId"] = "DisId trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(startLocationId))
            {
                ViewData["checking_startLocationId"] = "StartLocationId trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(endLocationId))
            {
                ViewData["checking_endLocationId"] = "EndLocationId trống!";
                return View();
            }
            else
            {
                var dbTours = new Models.Tours
                {
                    TourName = tourName,
                    Price = int.Parse(price),
                    StartDate = DateTime.Parse(startDate),
                    EndDate = DateTime.Parse(endDate),
                    Status = int.Parse(status),
                    CreationDate = DateTime.Parse(updateDate),
                    UpdateDate = DateTime.Parse(updateDate),
                    Remaining = int.Parse(remaining),
                    DiscountId = long.Parse(disId),
                    StartLocationId = long.Parse(startLocationId),
                    EndLocationId = long.Parse(endLocationId)
                };
                _db.tours.Add(dbTours);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Tour");
            }
        }
        #endregion

        #region Add Tour Days
        [HttpGet]
        public IActionResult Add_Tour_Days()
        {
            ViewBag.TourId = new SelectList(_db.tours.ToList().OrderBy(n => n.TourId), "TourId", "TourId");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add_Tour_Days(TourDays tourDays, IFormCollection f)
        {
            /*ViewBag.TourId = new SelectList(_db.tours.ToList().OrderBy(n => n.TourId), "TourId", "TourId");*/
            string dayNumber = f["DayNumber"].ToString();
            string description = f["Description"].ToString();
            string destination = f["Destination"].ToString();
            string time_Shedule = f["TimeSchedule"].ToString();
            string tourId = f["TourId"].ToString();

            if (string.IsNullOrEmpty(dayNumber))
            {
                ViewData["checking_dayNumber"] = "DayNumber trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(description))
            {
                ViewData["checking_Des"] = "Description trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(destination))
            {
                ViewData["checking_Destination"] = "Destination trống!";
                return View();
            }
            else if (string.IsNullOrEmpty(time_Shedule))
            {
                ViewData["checking_TimeSchedule"] = "TimeSchedule trống!";
                return View();
            }
            else
            {
                var dbTourDays = new Models.TourDays
                {
                    DayNumber = int.Parse(dayNumber),
                    Description = description,
                    Destination = destination,
                    TimeSchedule = DateTime.Parse(time_Shedule),
                    TourId = int.Parse(tourId)
                };
                _db.tourDays.Add(dbTourDays);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", "Tour");
            }

        }

        #endregion

        #region Payment
        [HttpGet]
        public IActionResult Payments()
        {
            // Lấy dữ liệu từ phiên
            var userId = HttpContext.Session.Get("user_id");
            var tourId = HttpContext.Session.GetInt32("tourId");
            var bookingId = HttpContext.Session.GetInt32("bookingId");
            var userAccount = HttpContext.Session.GetString("user_account");
            var fullName = HttpContext.Session.GetString("fullName");
            var email = HttpContext.Session.GetString("Email");

            // Chuyển đổi dữ liệu từ phiên
            Users dbUser = null;
            if (!string.IsNullOrEmpty(userAccount))
            {
                dbUser = JsonConvert.DeserializeObject<Users>(userAccount);
            }

            long? dbUserId = 0;
            if (userId != null)
            {
                dbUserId = BitConverter.ToInt64(userId);
            }

            long? dbTourId = 0;
            if (tourId != null)
            {
                dbTourId = Convert.ToInt64(tourId);
            }

            long? dbBookingId = 0;
            if (bookingId != null)
            {
                dbBookingId = Convert.ToInt64(bookingId);
            }

            var booking = _db.bookings.FirstOrDefault(t => t.BookingId == dbBookingId && t.TourId == tourId);
            ViewBag.Booking = booking;


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Payments(IFormCollection f)
        {
            // Lấy dữ liệu từ phiên
            var bookingId = f["BookingId"].ToString();
            var totalPrice = f["TotalMoney"].ToString();

            var payment = new Models.Payments
            {
                PaymentDate = DateTime.Now,
                BookingId = long.Parse(bookingId),
                TotalPrices = long.Parse(totalPrice),
                Amount = 1
            };
            _db.payments.Add(payment);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Tour");
        }

        #endregion

        #region Sorted by price
        [HttpGet]
        public IActionResult GetToursSortedByPriceAsc(int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            IPagedList<Tours> tours = _db.tours.OrderBy(t => t.Price).ToPagedList(pageNumber, pageSize);
            var tourImages = _db.tourImages.ToList();

            var tourImageUrls = new List<string>();
            foreach (var tour in tours)
            {
                var image = tourImages.FirstOrDefault(ti => ti.TourId == tour.TourId);
                if (image != null)
                {
                    tourImageUrls.Add(image.ImageUrl);
                }
            }

            ViewBag.TourImages = tourImageUrls;
            return PartialView("_ToursPricePartial", tours);
        }
        [HttpGet]
        public IActionResult GetToursSortedByPriceDesc(int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            IPagedList<Tours> tours= _db.tours.OrderByDescending(t => t.Price).ToPagedList(pageNumber, pageSize);
            var tourImages = _db.tourImages.ToList();

            var tourImageUrls = new List<string>();
            foreach (var tour in tours)
            {
                var image = tourImages.FirstOrDefault(ti => ti.TourId == tour.TourId);
                if (image != null)
                {
                    tourImageUrls.Add(image.ImageUrl);
                }
            }

            ViewBag.TourImages = tourImageUrls;
            return PartialView("_ToursPricePartial", tours);
        }

        #endregion

        #region Search by Location
        [HttpGet]
        public IActionResult GetToursByStartLocation(int? startLocationId, int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            IPagedList<Tours> TourList = _db.tours.ToPagedList(pageNumber, pageSize);
            var tourImages = _db.tourImages.ToList();

            var tourImageUrls = new List<string>();
            foreach (var tour in TourList)
            {
                var image = tourImages.FirstOrDefault(ti => ti.TourId == tour.TourId);
                if (image != null)
                {
                    tourImageUrls.Add(image.ImageUrl);
                }
            }
            if (startLocationId.HasValue)
            {
                TourList = _db.tours.Where(t => t.StartLocationId == startLocationId).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                TourList = _db.tours.ToPagedList(pageNumber, pageSize);
            }
            ViewBag.TourImages = tourImageUrls;
            return PartialView("_ToursPricePartial", TourList);
        }

        [HttpGet]
        public IActionResult GetToursByEndLocation(int? endLocationId, int? page)
        {
            int pageSize = 100;
            int pageNumber = (page ?? 1);
            IPagedList<Tours> TourList = _db.tours.ToPagedList(pageNumber, pageSize);
            var tourImages = _db.tourImages.ToList();

            var tourImageUrls = new List<string>();
            foreach (var tour in TourList)
            {
                var image = tourImages.FirstOrDefault(ti => ti.TourId == tour.TourId);
                if (image != null)
                {
                    tourImageUrls.Add(image.ImageUrl);
                }
            }
            if (endLocationId.HasValue)
            {
                TourList = _db.tours.Where(t => t.EndLocationId == endLocationId).ToPagedList(pageNumber, pageSize);
            }
            else
            {
                TourList = _db.tours.ToPagedList(pageNumber, pageSize);
            }
            ViewBag.TourImages = tourImageUrls;
            return PartialView("_ToursPricePartial", TourList);
        }
        #endregion
    }
}
