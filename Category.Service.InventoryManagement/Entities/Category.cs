using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CategoryService.Entities
{
    public partial class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string? CategoryName { get; set; }
    }
}
