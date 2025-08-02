
using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.Product.ViewModel;
using MenShopBlazor.DTOs.Receipt.CreateReceipt;
using MenShopBlazor.DTOs.Receipt.InputReceipt;
using MenShopBlazor.Services.Token;
using MenShopBlazor.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace MenShopBlazor.Services.InputReceiptService
{
    public class InputReceiptService : IInputReceiptService
    {
        private readonly HttpClient _httpClient;

        private const string baseUrl = "https://localhost:52221/api/InputReceipt";
        private readonly ITokenService _tokenService;

        public InputReceiptService(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            _tokenService = tokenService;
        }
        public async Task<List<InputReceiptDTO>?> GetAllInputReceiptsAsync()
        {
            try
            {
                var response = await HttpHelper.SendRequestAsync<List<InputReceiptDTO>>(() => _httpClient.GetAsync($"{baseUrl}"));
                if (response?.IsSuccess == true)
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gọi API: " + ex.Message);
            }
            return null;
        }
        public async Task<List<ProductDetailViewModel>?> GetInputReceiptByIdAsync(int idinput)
        {
            try
            {
                var response = await HttpHelper.SendRequestAsync<List<ProductDetailViewModel>>(() => _httpClient.GetAsync($"{baseUrl}/{idinput}"));
                if (response?.IsSuccess == true)
                {
                    return response.Data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi gọi API: " + ex.Message);
            }
            return null;
        }
        public async Task<ApiResponseModel<object>> CreateReceiptAsync(List<CreateReceiptDetailDTO> detailDTOs, string managerId)
        {
            var url = $"{baseUrl}/create?ManagerId={Uri.EscapeDataString(managerId)}";

            var response = await _httpClient.PostAsJsonAsync(url, detailDTOs);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseModel<object>>();
                return result ?? new ApiResponseModel<object>(false, "Không đọc được dữ liệu phản hồi", null, 500);
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                return new ApiResponseModel<object>(false, $"Lỗi API: {errorContent}", null, (int)response.StatusCode);
            }
        }
        public async Task<bool> CancelReceiptAsync(int id)
        {
            var response = await _httpClient.PutAsync($"{baseUrl}/cancel/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            // Optional: Log or process error response
            return false;
        }
        public async Task<bool> ConfirmReceiptAsync(int id)
        {
            var response = await _httpClient.PutAsync($"{baseUrl}/confirm/{id}", null);
            return response.IsSuccessStatusCode;
        }


    }
}
