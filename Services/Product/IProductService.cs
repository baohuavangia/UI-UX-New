using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.Product;
using MenShopBlazor.DTOs.Product.ReponseDTO;
using MenShopBlazor.DTOs.Product.UpdateProduct;
using MenShopBlazor.DTOs.Product.ViewModel;

namespace MenShopBlazor.Services.Product
{
    public interface IProductService
    {
        Task<List<CreateImageResponse>> AddImagesToDetailAsync(int detailId, List<string> imageUrls);
        Task<CreateProductDetailResponse> AddProductDetailsAsync(AddProductDetailDTO dto);
        Task<ProductResponseDTO?> CreateProductAsync(CreateProductDTO dto);
        Task<ApiResponseModel<object>> DeleteImageProductDetailAsync(int imageId);
        Task<ApiResponseModel<object>> DeleteProductAsync(int productId);
        Task<ApiResponseModel<object>> DeleteProductDetailAsync(int detailId);
        Task<List<ProductViewModel>> GetAllProductsAsync();
        Task<IEnumerable<ImageProductViewModel>> GetImageProductDetailsAsync(int productDetailId);
        Task<ProductViewModel?> GetProductByIdAsync(int id);
        Task<List<ProductDetailViewModel>> GetProductDetailsAsync(int productId);
        Task<List<ProductViewModel>> GetProductsByCategoryIdAsync(int categoryId);
        Task<ApiResponseModel<object>> ToggleProductStatusAsync(int productId);
        Task<ProductResponseDTO?> UpdateProductAsync(int productId, UpdateProductDTO dto);
        Task<ProductDetailResponse> UpdateProductDetailAsync(UpdateProductDetailDTO dto);
        Task<ImageResponse> UpdateProductDetailImagesAsync(int detailId, List<UpdateImageDTO> images);
    }
}