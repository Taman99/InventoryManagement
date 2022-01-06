using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CategoryService.Entities
{
    public partial class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string? Category1 { get; set; }
    }
}
