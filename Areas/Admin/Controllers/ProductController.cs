using DoAnPM_TH_.Models;
using DoAnPM_TH_.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAnPM_TH_.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly LongChauStoreContext _context;

        public ProductController(LongChauStoreContext context)
        {
            _context = context;
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
            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            var manufacturer = _context.Manufacturers.ToList();

            // Gán danh sách danh mục vào ViewBag
            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            ViewBag.Manufacturer = manufacturer;

            return View();
        }
        // POST: Product/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AdminAddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Tạo một đối tượng sản phẩm mới
                var product = new Product
                {
                    ProName = model.ProName,
                    CateId = model.CateId,
                    ManId = model.ManId,
                    BrandId = model.BrandId,
                    Unit = model.Unit,
                    Quantity = model.Quantity,
                    Price = model.Price,
                    Support = model.Support,
                    Ingredient = model.Ingredient,
                    Descript = model.Descript
                };

                // Lưu ảnh sản phẩm
                if (model.ProImg != null && model.ProImg.Length > 0)
                {
                    // Lưu ảnh vào thư mục bạn đã chỉ định
                    var fileName = Path.GetFileName(model.ProImg.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ProductImages", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ProImg.CopyToAsync(stream);
                    }

                    product.ProImg = "/images/ProductImages/" + fileName; // Lưu đường dẫn ảnh
                }

                // Thêm sản phẩm vào cơ sở dữ liệu
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Chuyển hướng đến danh sách sản phẩm
                return RedirectToAction("ProductList");
            }

            // Nếu có lỗi, hiển thị lại form
            var categories = _context.Categories.ToList();
            var brands = _context.Brands.ToList();
            var manufacturers = _context.Manufacturers.ToList();

            ViewBag.Categories = categories;
            ViewBag.Brands = brands;
            ViewBag.Manufacturer = manufacturers;

            return View(model);
        }

        // POST: Product/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}
