using System;
using System.Collections.Generic;

namespace CategoryService.Entities
{
    public partial class Size
    {
        public int SizeIndex { get; set; }
        public int? ProductId { get; set; }
        public string? Size1 { get; set; }
        public decimal? SizePrice { get; set; }
    }
}
