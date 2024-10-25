using DoAnPM_TH_.Models;
using DoAnPM_TH_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoAnPM_TH_.Controllers
{
    public class HomeController : Controller
    {
        private readonly LongChauStoreContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LongChauStoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult SearchAjax(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return Json(new List<Product>());
            }

            var products = _context.Products
                .Where(p => p.ProName.Contains(keyword))
                .Select(p => new
                {
                    p.ProId,
                    p.ProName,
                    p.Price,
                    p.Unit,
                    p.ProImg
                }).Take(4).ToList();
            return Json(products);
        }

        [HttpGet]
        public IActionResult SearchResult(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return View(new List<Product>());
            }

            var products = _context.Products.Where(p => p.ProName.Contains(keyword)).ToList();

            return View(products);
        }


        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();

            var products = _context.Products.ToList();

            var viewModel = new HomeViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult Doctor()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
