using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DoAnPM_TH_.Models;

namespace DoAnPM_TH_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly LongChauStoreContext _context;

        public ProductController(LongChauStoreContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.BrandOrigin)
                .Include(p => p.Cate)
                .Include(p => p.Man)
                .Include(p => p.ManCountry)
                .Include(p => p.ListProductImgs)
                .ToListAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.BrandOrigin)
                .Include(p => p.Cate)
                .Include(p => p.Man)
                .Include(p => p.ManCountry)
                .Include(p => p.ListProductImgs)
                .Include(p => p.OrderDetails)
                .Include(p => p.Ratings)
                .Include(p => p.Vouchers)
                .Include(p => p.Users)
                .FirstOrDefaultAsync(p => p.ProId == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.ProId }, product);
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, Product product)
        {
            if (id != product.ProId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Product/ByBrand/5
        [HttpGet("ByBrand/{brandId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByBrand(string brandId)
        {
            return await _context.Products
                .Where(p => p.BrandId == brandId)
                .Include(p => p.Brand)
                .Include(p => p.Cate)
                .ToListAsync();
        }

        // GET: api/Product/ByManufacturer/5
        [HttpGet("ByManufacturer/{manId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByManufacturer(string manId)
        {
            return await _context.Products
                .Where(p => p.ManId == manId)
                .Include(p => p.Man)
                .Include(p => p.Brand)
                .ToListAsync();
        }

        // GET: api/Product/ByCategory/5
        [HttpGet("ByCategory/{cateId}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string cateId)
        {
            return await _context.Products
                .Where(p => p.CateId == cateId)
                .Include(p => p.Cate)
                .Include(p => p.Brand)
                .ToListAsync();
        }

        // GET: api/Product/FilterByPrice
        [HttpGet("FilterByPrice")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByPriceRange(
            [FromQuery] double? minPrice,
            [FromQuery] double? maxPrice)
        {
            var query = _context.Products.AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            return await query
                .Include(p => p.Brand)
                .Include(p => p.Cate)
                .ToListAsync();
        }

        // GET: api/Product/FilterByRating
        [HttpGet("FilterByRating")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByRating(double minRating)
        {
            return await _context.Products
                .Where(p => p.Rating >= minRating)
                .Include(p => p.Brand)
                .Include(p => p.Ratings)
                .ToListAsync();
        }

        // GET: api/Product/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts([FromQuery] string query)
        {
            return await _context.Products
                .Where(p => p.ProName.Contains(query) ||
                           p.Descript.Contains(query) ||
                           p.Ingredient.Contains(query))
                .Include(p => p.Brand)
                .Include(p => p.Cate)
                .ToListAsync();
        }

        // PUT: api/Product/UpdateStatus/5
        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateProductStatus(string id, [FromBody] string status)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Status = status;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.ProId == id);
        }
    }
}