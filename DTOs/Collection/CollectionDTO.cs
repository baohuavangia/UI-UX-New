namespace MenShopBlazor.DTOs.Collection
{
    public class CollectionDTO
    {
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ICollection<string>? Images { get; set; }
        public bool Status { get; set; }
        public ICollection<CollectionDetailDTO>? CollectionDetails { get; set; }
    }
}
