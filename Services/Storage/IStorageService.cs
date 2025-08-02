using MenShopBlazor.DTOs.Product.ViewModel;
using MenShopBlazor.DTOs.Storage;

namespace MenShopBlazor.Services.Storage
{
    public interface IStorageService
    {
        Task<List<StorageDTO>?> GetAllProductsAsync();
        Task<List<ProductDetailViewModel>> GetByProductDetailId(int productId);
        Task<List<ProductViewModel>> GetByProductId(int storageId);
    }
}