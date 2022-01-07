using System;
using System.Collections.Generic;

namespace ProductService.Entities
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? MerchantId { get; set; }
        public string? ProductDesc { get; set; }
        public string? ImageUrl1 { get; set; }
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
        public string? ImageUrl4 { get; set; }
        public string? ImageUrl5 { get; set; }
        public string? ImageUrl6 { get; set; }
        public int? CategoryId { get; set; }
        public string? ProductTag { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductDiscount { get; set; }
        public bool? SizesExist { get; set; }
    }
}
