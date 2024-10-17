using DoAnPM_TH_.Models;

namespace DoAnPM_TH_.ViewModels
{
    public class ProductDetailModel
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public List<ListProductImg> ListProductImg { get; set; }
    }
}
