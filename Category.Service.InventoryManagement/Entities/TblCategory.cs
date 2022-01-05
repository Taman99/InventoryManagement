using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Category.Service.Entities
{
    public class TblCategory
    {
        public int CategoryId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string? Category { get; set; }
    }
}
