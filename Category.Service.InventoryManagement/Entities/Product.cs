using System;
using System.Collections.Generic;

namespace CategoryService.Entities
{
    public partial class Product
    {
        public Product()
        {
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? MerchantId { get; set; }
        public string? ProductDesc { get; set; }
        public int? CategoryId { get; set; }
        public string? ProductTag { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductDiscount { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
