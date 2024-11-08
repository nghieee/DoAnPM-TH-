using Azure.Core;
using DoAnPM_TH_.Models;
using DoAnPM_TH_.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DoAnPM_TH_.Controllers
{
    public class CategoryController : Controller
    {
        private readonly LongChauStoreContext _context;

        public CategoryController(LongChauStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? categoryId, string sortOrder)
        {
            categoryId = categoryId?.Trim();
            if (string.IsNullOrEmpty(categoryId))
            {
                return NotFound();
            }
            // Lấy danh mục dựa trên ID
            var category = _context.Categories.FirstOrDefault(c => c.CateId == categoryId);
            if (category == null)
            {
                return NotFound();
            }

            // Lấy danh sách sản phẩm thuộc danh mục
            var productsQuery = _context.Products.Where(p => p.CateId == categoryId);
            switch (sortOrder)
            {
                case "asc":
                    productsQuery = productsQuery.OrderBy(p => p.Price);
                    break;
                case "desc":
                    productsQuery = productsQuery.OrderByDescending(p => p.Price);
                    break;
                default:
                    break;
            }

            var products = productsQuery.ToList();

            var viewModel = new CategoryViewModel
            {
                Category = category,
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult ProductDetails(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProId == productId);
            if (product == null)
            {
                return NotFound();
            }

            var category = _context.Categories.FirstOrDefault(c => c.CateId == product.CateId);
            if (category == null)
            {
                return NotFound();
            }

            var manufacturer = _context.Manufacturers.FirstOrDefault(m => m.ManId == product.ManId);
            if (manufacturer == null)
            {
                return NotFound();
            }

            var productImages = _context.ListProductImgs.Where(img => img.ProId == productId).ToList();

            var viewModel = new ProductDetailModel
            {
                Product = product,
                Category = category,
                Manufacturer = manufacturer,
                ListProductImg = productImages
            };

            return View(viewModel);
        }
    }
}
