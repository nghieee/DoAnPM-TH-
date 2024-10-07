using DoAnPM_TH_.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAnPM_TH_.Controllers
{
    public class ProductController : Controller
    {
        private readonly LongChauStoreContext _context;

        public ProductController(LongChauStoreContext context)
        {
            _context = context;
        }

    }
}
