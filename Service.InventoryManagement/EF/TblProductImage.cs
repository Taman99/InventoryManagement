using System;
using System.Collections.Generic;

namespace Product.Service.EF
{
    public partial class TblProductImage
    {
        public int PdtId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual TblProduct Pdt { get; set; } = null!;
    }
}
