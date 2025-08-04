﻿namespace MenShopBlazor.DTOs.DiscountPrice
{
    public class CreateDiscountPriceDTO
    {
        public string Name { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
