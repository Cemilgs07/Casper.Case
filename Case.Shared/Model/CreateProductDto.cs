using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Shared.Model
{
    public class CreateProductDto
    {
      
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        public int StokCount { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
    }
}
