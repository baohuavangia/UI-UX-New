using MenShopBlazor.DTOs.Product.ViewModel;

namespace MenShopBlazor.DTOs.Collection
{
    public class CollectionDetailDTO
    {
        public int CollectionDetailId { get; set; }
        public int CollectionId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public List<ProductDetailViewModel> ProductDetails { get; set; } = new();
    }
}
