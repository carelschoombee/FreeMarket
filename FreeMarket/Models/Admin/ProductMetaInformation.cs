using System;

namespace FreeMarket.Models
{
    public class ProductMetaInformation
    {
        public Product Product { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalWeightSold { get; set; }
        public DateTime? LastPurchased { get; set; }
    }
}