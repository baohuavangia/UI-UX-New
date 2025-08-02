namespace MenShopBlazor.DTOs.Collection
{
    public class CreateCollectionDTO
    {
        public string CollectionName { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool Status { get; set; }
    }
}
