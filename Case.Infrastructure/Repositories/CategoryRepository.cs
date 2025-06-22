using Case.Domain.Entities;
using Case.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Infrastructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbSet<Category> set) : base(set) { }

        public async Task<Category> CreateAsync(Category category)
        {
            var result = await _entities.Where(row => row.Name == category.Name).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Deleted = null;
                result.IsActive = true;

                return result;
            }
            await _entities.AddAsync(category);
            return category;
        }
    }
}
