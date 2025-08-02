using MenShopBlazor.DTOs;
using MenShopBlazor.DTOs.Collection;

namespace MenShopBlazor.Services.Collection
{
    public interface ICollectionService
    {
        Task<ApiResponseModel<object>> AddCollection(CreateCollectionDTO dto);
        Task<ApiResponseModel<object>> AddCollectionDetail(CreateCollectionDetailDTO dto);
        Task<ApiResponseModel<object>> DeleteCollection(int collectionId);
        Task<ApiResponseModel<object>> DeleteCollectionDetail(int detailId);
        Task<List<CollectionDTO>?> GetAllCollection();
        Task<List<CollectionDetailDTO>?> GetAllCollectionDetailById(int collectionId);
        Task<CollectionDTO> GetCollectionByID(int collectionId);
        Task<ApiResponseModel<object>> UpdateCollection(CollectionDTO dto);
        Task<ApiResponseModel<object>> UpdateCollectionDetail(int detailId, CreateCollectionDetailDTO dto);
        Task<ApiResponseModel<object>> UpdateCollectionStatus(int collectionId, bool status);
    }
}