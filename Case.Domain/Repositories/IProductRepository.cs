using Case.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Domain.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> CreateAsync(Product category);
        Task<List<Product>> GetAllWithCategoryAsync();


    }
}
