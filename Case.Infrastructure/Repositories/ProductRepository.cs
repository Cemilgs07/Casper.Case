using Case.Domain.Entities;
using Case.Domain.Repositories;
using Case.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DbSet<Product> set) : base(set) { }

        public async Task<Product> CreateAsync(Product product)
        {
            var result = await _entities.Where(row => row.IsActive && row.Name == product.Name && row.CategoryId == product.CategoryId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Deleted = null;
                result.IsActive = true;
                return result;
            }
            await _entities.AddAsync(product);
            return product;

        }

        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _entities.Where(row=>row.IsActive).Include(p => p.Category)
                .ToListAsync();
        }
    }
}
