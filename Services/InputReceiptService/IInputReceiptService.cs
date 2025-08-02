using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.Product.ViewModel;
using MenShopBlazor.DTOs.Receipt.CreateReceipt;
using MenShopBlazor.DTOs.Receipt.InputReceipt;

namespace MenShopBlazor.Services.InputReceiptService
{
    public interface IInputReceiptService
    {
        Task<bool> CancelReceiptAsync(int id);
        Task<bool> ConfirmReceiptAsync(int id);
        Task<ApiResponseModel<object>> CreateReceiptAsync(List<CreateReceiptDetailDTO> detailDTOs, string managerId);
        Task<List<InputReceiptDTO>?> GetAllInputReceiptsAsync();
        Task<List<ProductDetailViewModel>?> GetInputReceiptByIdAsync(int idinput);
    }
}