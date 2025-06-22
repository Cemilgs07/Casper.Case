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
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
    {
        protected readonly DbSet<TEntity> _entities;

        public BaseRepository(DbSet<TEntity> entities)
        {
            _entities = entities;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.Where(row => row.IsActive).ToListAsync();
        }
        public virtual async Task<int> GetCount()
        {
            return await _entities.Where(row => row.IsActive).CountAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _entities.Where(row => row.IsActive).FirstOrDefaultAsync(row => row.Id == id);
        }

        public virtual async Task<List<TEntity>> GetListByIdsAsync(List<Guid> ids)
        {
            return await _entities.Where(row => row.IsActive).Where(row => ids.Contains(row.Id)).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetListAsync()
        {
            return await _entities.Where(row => row.IsActive).ToListAsync();
        }
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var row = await GetByIdAsync(id);
            if (row == null)
                return false;

            row.Deleted = DateTime.UtcNow;
            row.IsActive = false;
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _entities.AnyAsync(row => row.Id == id && row.IsActive);
        }
    }
}
