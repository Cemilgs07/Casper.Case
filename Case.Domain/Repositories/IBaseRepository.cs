using Case.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Domain.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> GetCount();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>> GetListByIdsAsync(List<Guid> ids);
        Task<List<TEntity>> GetListAsync();
        Task<bool> DeleteByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
