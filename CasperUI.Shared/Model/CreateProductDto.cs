using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasperUI.Shared.Model
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public int StokCount { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public List<CategoryDto>? Categories { get; set; }

    }

}
