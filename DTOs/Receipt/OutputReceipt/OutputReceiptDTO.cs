using MenShopBlazor.DTOs.Product.ViewModel;

namespace MenShopBlazor.DTOs.Receipt.OutputReceipt
{
    public class OutputReceiptDTO
    {
        public int ReceiptId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public string? Status { get; set; }
        public decimal? Total { get; set; }
        public string? ManagerName { get; set; }
        public string? BranchName { get; set; }
        public ICollection<ProductDetailViewModel>? OutputReceiptDetails { get; set; }
    }
}
