﻿using System;
using System.Collections.Generic;

namespace IMServices.Entities
{
    public partial class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? UserId { get; set; }
    }
}
