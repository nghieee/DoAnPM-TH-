using DoAnPM_TH_.Models;
using DoAnPM_TH_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnPM_TH_.Controllers
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

        public IActionResult ProductList(int page = 1, int pageSize = 10)
        {
            var totalItems = _context.Products.Count();
            var products = _context.Products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(product => new AdminProductListViewModel
                {
                    Product = product,
                    Category = _context.Categories.FirstOrDefault(c => c.CateId == product.CateId),
                    Manufacturer = _context.Manufacturers.FirstOrDefault(m => m.ManId == product.ManId),
                    Brand = _context.Brands.FirstOrDefault(b => b.BrandId == product.BrandId)
                })
                .ToList();

            var viewModel = new PagedProductListViewModel
            {
                Products = products,
                PageSize = pageSize,
                CurrentPage = page,
                TotalItems = totalItems
            };

            return View(viewModel);
        }

        // GET: Product/Add
        public IActionResult AddProduct()
        {
            return View();
        }

        // POST: Product/Add
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile proImg)
        {
            if (ModelState.IsValid)
            {
                // Xử lý hình ảnh
                if (proImg != null && proImg.Length > 0)
                {
                    var fileName = Path.GetFileName(proImg.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName); // Đường dẫn lưu ảnh

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await proImg.CopyToAsync(stream);
                    }

                    product.ProImg = "/images/" + fileName; // Lưu đường dẫn vào thuộc tính ProImg
                }

                // Lưu sản phẩm vào database
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("ProductList"); // Chuyển hướng đến danh sách sản phẩm
            }

            return View(product); // Trả về view với model đã nhập nếu có lỗi
        }
    }
}
