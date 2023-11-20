using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HKQTravelling.Models;
namespace HKQTravelling.Controllers
{
    public class ChatHubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ApplicationDBContext _data;

        public ChatHubController(ApplicationDBContext data)
        {
            this._data = data;
        }
    }
}


