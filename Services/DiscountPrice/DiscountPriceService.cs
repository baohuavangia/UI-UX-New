using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.DiscountPrice;
using MenShopBlazor.DTOs.Product.ReponseDTO;
using MenShopBlazor.Shared;
using Newtonsoft.Json;
using System.Text;

namespace MenShopBlazor.Services.DiscountPrice
{
    public class DiscountPriceService : IDiscountPriceService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://localhost:52221/api/DiscountPrice";

        public DiscountPriceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DiscountPriceViewModel>> GetAllDiscountPrice()
        {
            try
            {
                var response = await _httpClient.GetAsync(baseUrl);
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DiscountPriceViewModel>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy danh sách khuyến mãi: {ex.Message}");
                return new();
            }
        }

        public async Task<DiscountPriceViewModel> GetDiscountPriceById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/{id}");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<DiscountPriceViewModel>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy khuyến mãi theo ID: {ex.Message}");
                return new();
            }
        }

        public async Task<DiscountPriceDetailViewModel?> GetDiscountPriceDetailById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/detail/product/{id}");
                var json = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<DiscountPriceDetailViewModel>>(json);
                return list?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy chi tiết khuyến mãi theo sản phẩm: {ex.Message}");
                return null;
            }
        }

        public async Task<List<DiscountPriceDetailViewModel>> GetAllProductDetailDiscountPrice(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/detail/{id}");
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<DiscountPriceDetailViewModel>>(json) ?? new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi lấy tất cả chi tiết sản phẩm của khuyến mãi: {ex.Message}");
                return new();
            }
        }

        public async Task<DiscountPriceResponse?> CreateDiscountPrice(CreateDiscountPriceDTO dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseUrl}", content);
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DiscountPriceResponse>(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi tạo khuyến mãi: {ex.Message}");
                return new DiscountPriceResponse { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<DiscountPriceDetailResponse> CreateDiscountPriceDetail(CreateDiscountPriceDetailDTO dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{baseUrl}/detail", content);
                var result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.MultiStatus)
                {
                    return new DiscountPriceDetailResponse
                    {
                        Results = new List<ProductResult>
                        {
                            new ProductResult
                            {
                                IsSuccess = false,
                                Message = $"207: Một số sản phẩm đã có khuyến mãi hoặc bị lỗi khi thêm.\nChi tiết: {result}"
                            }
                        }
                    };
                }

                if (!response.IsSuccessStatusCode)
                {
                    return new DiscountPriceDetailResponse
                    {
                        Results = new List<ProductResult>
                        {
                            new ProductResult
                            {
                                IsSuccess = false,
                                Message = $"Lỗi: {response.StatusCode} - {result}"
                            }
                        }
                    };
                }

                return JsonConvert.DeserializeObject<DiscountPriceDetailResponse>(result)
                    ?? new DiscountPriceDetailResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi thêm chi tiết khuyến mãi: {ex.Message}");
                return new DiscountPriceDetailResponse
                {
                    Results = new List<ProductResult>
                    {
                        new()
                        {
                            IsSuccess = false,
                            Message = ex.Message
                        }
                    }
                };
            }
        }

        public async Task<DiscountPriceResponse?> UpdateDiscountPrice(int id, CreateDiscountPriceDTO dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{baseUrl}/{id}", content);
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DiscountPriceResponse>(result)
                    ?? new DiscountPriceResponse { IsSuccess = false, Message = "Phản hồi rỗng từ server." };
            }
            catch (Exception ex)
            {
                return new DiscountPriceResponse { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<DiscountPriceDetailResponsenew> UpdateDiscountPriceDetail(UpdateDiscountPriceDTO dto)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{baseUrl}/detail", content);
                var result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<DiscountPriceDetailResponsenew>(result)
                    ?? new DiscountPriceDetailResponsenew { IsSuccess = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật chi tiết khuyến mãi: {ex.Message}");
                return new DiscountPriceDetailResponsenew { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponseModel<object>> DeleteDiscountPriceAsync(int productId)
        {
            return await HttpHelper.SendRequestAsync<object>(() =>
                _httpClient.DeleteAsync($"{baseUrl}/{productId}")
            );
        }

        public async Task<ApiResponseModel<object>> DeleteDiscountPriceDetailAsync(int id)
        {
            return await HttpHelper.SendRequestAsync<object>(() =>
                _httpClient.DeleteAsync($"{baseUrl}/detail/{id}")
            );
        }
    }
}
