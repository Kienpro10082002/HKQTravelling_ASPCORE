using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HKQTravelling.Models;
namespace HKQTravelling.Controllers
{
    public class TourCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDBContext _data;

        public TourCartController(ApplicationDBContext data)
        {
            this._data = data;
        }
/*
        private List<TourCartModel> GetBill_Tours()
        {
            List<TourCartModel> bill_Tours = HttpContext.Session.Get<List<TourCartModel>>("bill_tour");
            if (bill_Tours == null)
            {
                bill_Tours = new List<TourCartModel>();
                HttpContext.Session.Set("bill_tour", bill_Tours);
            }
            return bill_Tours;
        }

        public IActionResult Add_TourCart(long id, string strURL)
        {
            List<TourCartModel> lsbill_Tours = GetBill_Tours();
            TourCartModel tourCartss = lsbill_Tours.Find(n => n.iIdTour == id);
            if (tourCartss == null)
            {
                tourCartss = new TourCartModel(id);
                lsbill_Tours.Add(tourCartss);
            }
            else
            {
                tourCartss.iQuantity++;
            }
            return RedirectToAction("TourCart");
        }

        public int TotalQuatityTour()
        {
            int itotalQuatityTour = 0;
            List<TourCartModel> lsbill_tour = HttpContext.Session.Get<List<TourCartModel>>("bill_tour");

            if (lsbill_tour != null)
            {
                itotalQuatityTour = lsbill_tour.Sum(n => n.iQuantity);
            }
            return itotalQuatityTour;
        }

        public decimal TotalPriceTour()
        {
            decimal dTotalpriceTour = 0;
            List<TourCartModel> lsbill_tours = HttpContext.Session.Get<List<TourCartModel>>("bill_tour");
            if (lsbill_tours != null)
            {
                dTotalpriceTour = lsbill_tours.Sum(n => n.dTotalpriceTour.GetValueOrDefault());
            }
            return dTotalpriceTour;
        }

        public long IdTour()
        {
            long idTour = 0;
            List<TourCartModel> lsbill_Tours = HttpContext.Session.Get<List<TourCartModel>>("bill_tour");
            if (lsbill_Tours != null)
            {
                idTour = lsbill_Tours.Sum(n => n.iIdTour);
            }
            return idTour;
        }

        public IActionResult TourCart()
        {
            List<TourCartModel> lsbill_tours = GetBill_Tours();
            if (lsbill_tours.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.dTotalQuatityTour = TotalQuatityTour();
            ViewBag.dTotalpriceTour = TotalPriceTour();
            return View(lsbill_tours);
        }

        public IActionResult TourCartPartial()
        {
            ViewBag.dTotalpriceTour = TotalPriceTour();
            ViewBag.dTotalQuatityTour = TotalQuatityTour();
            return PartialView();
        }

        public IActionResult DeleteTourCart(int id)
        {
            List<TourCartModel> lstourCarts = GetBill_Tours();
            TourCartModel tourCarts = lstourCarts.SingleOrDefault(n => n.iIdTour == id);
            if (lstourCarts.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            if (tourCarts != null)
            {
                lstourCarts.RemoveAll(n => n.iIdTour == id);
                return RedirectToAction("TourCart");
            }
            return RedirectToAction("Travel", "Home");
        }

        public IActionResult UpdateTourCart(int id, int txtSoluong)
        {
            List<TourCartModel> tourCartss = GetBill_Tours();
            TourCartModel tourCarts = tourCartss.SingleOrDefault(n => n.iIdTour == id);
            if (tourCarts != null)
            {
                tourCarts.iQuantity = txtSoluong;
            }
            return RedirectToAction("TourCart");
        }

        public IActionResult DeleteAllTourCart()
        {
            List<TourCartModel> tourCartss = GetBill_Tours();
            tourCartss.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult BookingTour()
        {
            if (HttpContext.Session.GetString("user_account") == null || HttpContext.Session.GetString("user_account") == "")
            {
                return RedirectToAction("Login", "Guest");
            }
            if (HttpContext.Session.GetString("user_account") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            List<TourCartModel> lstGiohang = GetBill_Tours();
            ViewBag.dTotalQuatityTour = TotalQuatityTour();
            ViewBag.dTotalpriceTour = TotalPriceTour();
            ViewBag.lIdTour = IdTour();

            return View(lstGiohang);
        }

        [HttpPost]
        public IActionResult BookingTour(FormCollection f)
        {
            bill_tour bill_Tours = new bill_tour();
            user_account user = HttpContext.Session.Get<user_account>("user_account");
            List<TourCartModel> tourCarts = GetBill_Tours();
            bill_Tours.user_id = user.user_id;
            bill_Tours.create_date = DateTime.Now;
            foreach (var item in tourCarts)
            {
                bill_Tours.quantity = item.iQuantity;
                bill_Tours.total = item.dTotalpriceTour;
                bill_Tours.tour_id = item.iIdTour;
            }
            _data.bill_tours.Add(bill_Tours);
            _data.SaveChanges();
            HttpContext.Session.Set("bill_tour", null);
            return RedirectToAction("SubmitTourCart");
        }*/

        public IActionResult SubmitTourCart()
        {
            return View();
        }
    }
}


