﻿using System;
using System.Collections.Generic;

namespace Product.Service.EF
{
    public partial class ProductImage
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;
    }
}