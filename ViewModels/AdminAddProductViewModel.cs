using System.ComponentModel.DataAnnotations;

namespace DoAnPM_TH_.ViewModels
{
    public class AdminAddProductViewModel
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
        public string ProName { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục.")]
        public string CateId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn nhà sản xuất.")]
        public string ManId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn thương hiệu.")]
        public string BrandId { get; set; }

        [Required(ErrorMessage = "Đơn vị không được để trống.")]
        public string Unit { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Đơn giá không được để trống.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 0.")]
        public double Price { get; set; }

        public string Support { get; set; }

        public string Ingredient { get; set; }

        public string Descript { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ảnh sản phẩm.")]
        public IFormFile ProImg { get; set; }
    }

}
