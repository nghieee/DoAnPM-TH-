using Microsoft.AspNetCore.Mvc;

namespace DoAnPM_TH_.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
