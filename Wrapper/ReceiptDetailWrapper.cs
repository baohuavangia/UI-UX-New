
using MenShopBlazor.DTOs.Product.ViewModel;
using MenShopBlazor.DTOs.Receipt.CreateReceipt;


namespace MenShopBlazor.Wrapper
{
	public class ReceiptDetailWrapper : CreateReceiptDetailDTO
	{
		public string? SelectedFabricName { get; set; }
		public string? SelectedColorName { get; set; }
		public string? SelectedSizeName { get; set; }

		public List<string> FabricNames { get; set; } = new();
		public List<string> ColorNames { get; set; } = new();
		public List<string> SizeNames { get; set; } = new();

		public List<ProductDetailViewModel> FilteredProductDetails { get; set; } = new();
	}

}
