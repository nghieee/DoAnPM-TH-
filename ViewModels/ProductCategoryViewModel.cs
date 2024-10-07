using DoAnPM_TH_.Models;

namespace DoAnPM_TH_.ViewModels
{
    public class ProductCategoryViewModel
    {
        public Category Category { get; set; } // Danh mục
        public List<Product> Products { get; set; } // Danh sách sản phẩm thuộc danh mục
    }
}
