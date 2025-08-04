using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.DiscountPrice;

namespace MenShopBlazor.Services.DiscountPrice
{
    public interface IDiscountPriceService
    {
        Task<DiscountPriceResponse?> CreateDiscountPrice(CreateDiscountPriceDTO dto);
        Task<DiscountPriceDetailResponse> CreateDiscountPriceDetail(CreateDiscountPriceDetailDTO dto);
        Task<ApiResponseModel<object>> DeleteDiscountPriceAsync(int productId);
        Task<ApiResponseModel<object>> DeleteDiscountPriceDetailAsync(int id);
        Task<List<DiscountPriceViewModel>> GetAllDiscountPrice();
        Task<List<DiscountPriceDetailViewModel>> GetAllProductDetailDiscountPrice(int id);
        Task<DiscountPriceViewModel> GetDiscountPriceById(int id);
        Task<DiscountPriceDetailViewModel> GetDiscountPriceDetailById(int id);
        Task<DiscountPriceResponse?> UpdateDiscountPrice(int productId, CreateDiscountPriceDTO dto);
        Task<DiscountPriceDetailResponsenew> UpdateDiscountPriceDetail(UpdateDiscountPriceDTO dto);
    }
}