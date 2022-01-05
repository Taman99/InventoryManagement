using System;
using System.Collections.Generic;

namespace Sizes.Service.EF
{
    public partial class TblSize
    {
        public int SizeIndex { get; set; }
        public int? PdtId { get; set; }
        public string? Size { get; set; }
        public decimal? SizePrice { get; set; }
    }
}
