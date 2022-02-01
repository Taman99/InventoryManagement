using System;
using System.Collections.Generic;

namespace IMServices.Entities
{
    public partial class Size
    {
        public int SizeId { get; set; }
        public string? ProductId { get; set; }
        public string? SizeName { get; set; }
        public decimal? SizePrice { get; set; }
    }
}
