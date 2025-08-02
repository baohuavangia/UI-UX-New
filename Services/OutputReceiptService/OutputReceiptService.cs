using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.Product.ViewModel;
using MenShopBlazor.DTOs.Receipt.CreateReceipt;
using MenShopBlazor.DTOs.Receipt.OutputReceipt;
using MenShopBlazor.Services.Token;
using MenShopBlazor.Shared;
using System.Net.Http;
using System.Net.Http.Json;

namespace MenShopBlazor.Services.OutputReceiptService
{
    public class OutputReceiptService : IOutputReceiptService
    {
        private readonly HttpClient _httpClient;


        private const string baseUrl = "https://localhost:52221/api/OutputReceipt";
        private readonly ITokenService _tokenService;

        public OutputReceiptService(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            _tokenService = tokenService;
        }
        public async Task<List<OutputReceiptDTO>?> GetAllOutputReceiptsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponseModel<List<OutputReceiptDTO>>>($"{baseUrl}");
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
        public async Task<List<ProductDetailViewModel>?> GetOutputDetailReceiptDTOs(int idoutput)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponseModel<List<ProductDetailViewModel>>>($"{baseUrl}/{idoutput}");
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
        public async Task<List<OutputReceiptDTO>?> GteOuputBranch(int branchId)
        {
            try
            {
                var response = await HttpHelper.SendRequestAsync<List<OutputReceiptDTO>>(() => _httpClient.GetAsync($"{baseUrl}/branch/{branchId}"));
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
        public async Task<ApiResponseModel<object>> CreateReceiptAsync(List<CreateReceiptDetailDTO> detailDTOs, string managerId, int? branchId)
        {
            var query = $"?branchId={branchId}&managerId={managerId}";
            var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/create{query}", detailDTOs);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseModel<object>>();
                return result!;
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<ApiResponseModel<object>>();
                return error ?? new ApiResponseModel<object>(false, "Lỗi không xác định", null, (int)response.StatusCode);
            }
        }
    }
}
