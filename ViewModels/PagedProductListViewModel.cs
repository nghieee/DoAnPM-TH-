namespace DoAnPM_TH_.ViewModels
{
    public class PagedProductListViewModel
    {
        public List<AdminProductListViewModel> Products { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }

        // Tính tổng số trang dựa vào tổng số sản phẩm và số lượng sản phẩm trên mỗi trang
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
