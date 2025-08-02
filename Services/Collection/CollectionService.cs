using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.Collection;
using MenShopBlazor.Services.Token;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace MenShopBlazor.Services.Collection
{
    public class CollectionService : ICollectionService
    {
        private readonly HttpClient _httpClient;
        private const string baseUrl = "https://localhost:52221/api/Collections";
        private readonly ITokenService _tokenService;

        public CollectionService(IHttpClientFactory httpClientFactory, ITokenService tokenService)
        {
            _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
            _tokenService = tokenService;
        }

        public async Task<List<CollectionDTO>?> GetAllCollection()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}");
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CollectionDTO>>(json);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CollectionDTO> GetCollectionByID(int collectionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/{collectionId}");
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CollectionDTO>(json);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CollectionDetailDTO>?> GetAllCollectionDetailById(int collectionId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/{collectionId}/details");
                if (!response.IsSuccessStatusCode) return null;

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CollectionDetailDTO>>(json);
            }
            catch
            {
                return null;
            }
        }

        public async Task<ApiResponseModel<object>> AddCollection(CreateCollectionDTO dto)
        {
            return await SendJsonRequest($"{baseUrl}/createcollection", dto, HttpMethod.Post);
        }

        public async Task<ApiResponseModel<object>> UpdateCollection(CollectionDTO dto)
        {
            return await SendJsonRequest(
                $"{baseUrl}/update-collection/{dto.CollectionId}",
                new
                {
                    dto.CollectionName,
                    dto.Description,
                    dto.StartTime,
                    dto.EndTime
                },
                HttpMethod.Put
            );
        }

        public async Task<ApiResponseModel<object>> DeleteCollection(int collectionId)
        {
            return await SendRawRequest($"{baseUrl}/delete-collection/{collectionId}", HttpMethod.Delete);
        }

        public async Task<ApiResponseModel<object>> AddCollectionDetail(CreateCollectionDetailDTO dto)
        {
            return await SendJsonRequest(
                $"{baseUrl}/add-details",
                new { dto.CollectionId, dto.ProductId },
                HttpMethod.Post
            );
        }

        public async Task<ApiResponseModel<object>> UpdateCollectionDetail(int detailId, CreateCollectionDetailDTO dto)
        {
            return await SendJsonRequest(
                $"{baseUrl}/details/{detailId}",
                new { dto.ProductId },
                HttpMethod.Put
            );
        }

        public async Task<ApiResponseModel<object>> DeleteCollectionDetail(int detailId)
        {
            return await SendRawRequest($"{baseUrl}/details/{detailId}", HttpMethod.Delete);
        }

        // 🛠 Helper: POST / PUT requests with JSON body
        private async Task<ApiResponseModel<object>> SendJsonRequest(string url, object payload, HttpMethod method)
        {
            try
            {
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = method switch
                {
                    HttpMethod m when m == HttpMethod.Post => await _httpClient.PostAsync(url, content),
                    HttpMethod m when m == HttpMethod.Put => await _httpClient.PutAsync(url, content),
                    _ => throw new NotSupportedException("Unsupported HTTP method")
                };

                var result = await response.Content.ReadAsStringAsync();

                return new ApiResponseModel<object>(
                    isSuccess: response.IsSuccessStatusCode,
                    message: response.IsSuccessStatusCode
                        ? "Thành công"
                        : $"Thất bại: {(int)response.StatusCode} - {response.ReasonPhrase}",
                    data: response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<object>(result) : null,
                    statusCode: (int)response.StatusCode
                );
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<object>(
                    isSuccess: false,
                    message: $"Lỗi gửi request: {ex.Message}",
                    data: null,
                    statusCode: 500
                );
            }
        }

        // 🛠 Helper: DELETE (or GET if you want to expand it)
        private async Task<ApiResponseModel<object>> SendRawRequest(string url, HttpMethod method)
        {
            try
            {
                HttpResponseMessage response = method switch
                {
                    HttpMethod m when m == HttpMethod.Delete => await _httpClient.DeleteAsync(url),
                    _ => throw new NotSupportedException("Unsupported HTTP method")
                };

                var result = await response.Content.ReadAsStringAsync();

                return new ApiResponseModel<object>(
                    isSuccess: response.IsSuccessStatusCode,
                    message: response.IsSuccessStatusCode
                        ? "Xóa thành công"
                        : $"Xóa thất bại: {(int)response.StatusCode} - {response.ReasonPhrase}",
                    data: response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<object>(result) : null,
                    statusCode: (int)response.StatusCode
                );
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<object>(
                    isSuccess: false,
                    message: $"Lỗi gửi request DELETE: {ex.Message}",
                    data: null,
                    statusCode: 500
                );
            }
        }
        public async Task<ApiResponseModel<object>> UpdateCollectionStatus(int collectionId, bool status)
        {
            var payload = new UpdateStatusDTO { Status = status };

            try
            {
                var json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PatchAsync($"{baseUrl}/{collectionId}/status", content);
                var result = await response.Content.ReadAsStringAsync();

                return new ApiResponseModel<object>(
                    isSuccess: response.IsSuccessStatusCode,
                    message: response.IsSuccessStatusCode
                        ? "Cập nhật trạng thái thành công"
                        : $"Lỗi: {(int)response.StatusCode} - {response.ReasonPhrase}",
                    data: response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<object>(result) : null,
                    statusCode: (int)response.StatusCode
                );
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<object>(
                    isSuccess: false,
                    message: $"Lỗi gửi request PATCH: {ex.Message}",
                    data: null,
                    statusCode: 500
                );
            }
        }

    }
}
