using DoAnPM_TH_.Models;
using DoAnPM_TH_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnPM_TH_.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly LongChauStoreContext _context;

        public AdminController(LongChauStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
