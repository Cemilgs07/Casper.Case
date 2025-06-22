using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasperUI.Shared.Model
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int StockCount { get; set; }
        public Guid CategoryId { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
    }
}
