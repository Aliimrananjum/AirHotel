using Microsoft.AspNetCore.Mvc;

namespace AirHotel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
