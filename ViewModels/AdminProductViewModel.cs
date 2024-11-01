namespace DoAnPM_TH_.ViewModels
{
    public class AdminProductViewModel
    {
        public string ProName { get; set; }
        public double? Price { get; set; }
        public string? Unit { get; set; }
        public int? Quantity { get; set; }
        public string? CateId { get; set; }
        public string? BrandId { get; set; }
        public string? ManId { get; set; }
        public string? Descript { get; set; }
        public string? Support { get; set; }
        public string? Ingredient { get; set; }
        public IFormFile? ProImg { get; set; } 
    }
}
