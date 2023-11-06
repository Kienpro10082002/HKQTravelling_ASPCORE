//using database
using HKQTravelling.Extension;
using HKQTravelling.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace HKQTravelling.Controllers
{
    public class TourController : Controller
    {
        //khai báo hàm app 
        private readonly ApplicationDBContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public TourController(ApplicationDBContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Tours> objTourList = _db.tours.ToList();
            return View(objTourList);
        }

        #region DetailTour
        [HttpGet]

        public IActionResult DetailTour(long id)
        {
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



        #region Add Tour
        [HttpGet]
        public IActionResult AddTour()
        {
            ViewBag.StartLocationId = new SelectList(_db.startLocations.ToList().OrderBy(n => n.StartLocationName), "StartLocationId", "StartLocationName");
            ViewBag.EndLocationId = new SelectList(_db.endLocations.ToList().OrderBy(n => n.EndLocationName), "EndLocationId", "EndLocationName");
            ViewBag.DisId = new SelectList(_db.discounts.ToList().OrderBy(n => n.DiscountId), "DiscountId", "DiscountName");
            ViewBag.RuleId = new SelectList(_db.rules.ToList().OrderBy(n => n.RuleId), "RuleId", "RuleName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTour(Tours tours, TourImages tourImages, IFormFile fileBase)
        {
            ViewBag.StarLocationId = new SelectList(_db.startLocations.ToList().OrderBy(n => n.StartLocationName), "StartLocationId", "StartLocationName");
            ViewBag.EndLocationId = new SelectList(_db.endLocations.ToList().OrderBy(n => n.EndLocationName), "EndLocationId", "EndLocationName");
            ViewBag.DisId = new SelectList(_db.discounts.ToList().OrderBy(n => n.DiscountId), "DiscountId", "DiscountName");
            ViewBag.RuleId = new SelectList(_db.rules.ToList().OrderBy(n => n.RuleId), "RuleId", "RuleName");

            if (fileBase == null)
            {
                ViewBag.Error = "Chon anh bia!";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileBase.FileName);
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.ThongBao = "Hinh anh da ton tai!";
                    }
                    else
                    {
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await fileBase.CopyToAsync(stream);
                        }
                    }
                    tourImages.ImageUrl = "~/img/" + fileName; // Lưu đường dẫn tương đối của tệp
                    _db.tours.Add(tours); // Thêm tour vào database
                    _db.SaveChanges(); // Lưu thay đổi
                }
            }

            return RedirectToAction("Index");
        }
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
            else if(string.IsNullOrEmpty(time_Shedule))
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
    }
}
