using Case.Domain.Repositories;
using Case.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CaseAPIDbContext _context;

        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategorytRepository { get; }

        public UnitOfWork(CaseAPIDbContext context,
                          IProductRepository productRepository,
                          ICategoryRepository categorytRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            CategorytRepository = categorytRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
