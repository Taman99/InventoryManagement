using System;
using System.Collections.Generic;

namespace Product.Service.EF
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblProductImages = new HashSet<TblProductImage>();
        }

        public int PdtId { get; set; }
        public string? PdtName { get; set; }
        public string? UserId { get; set; }
        public string? PdtDesc { get; set; }
        public int? PdtCategoryId { get; set; }
        public string? PdtTag { get; set; }
        public int? PdtQuantity { get; set; }
        public decimal? PdtPrice { get; set; }
        public decimal? PdtDiscount { get; set; }

        public virtual ICollection<TblProductImage> TblProductImages { get; set; }
    }
}
