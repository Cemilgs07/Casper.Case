using Case.Domain.Entities;
using Case.Domain.Repositories;
using Case.Infrastructure.Persistance;
using Case.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        
        public static IServiceCollection ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>(f =>
            {
                var db = f.GetRequiredService<CaseAPIDbContext>();
                if (db == null) throw new ArgumentNullException(nameof(db));

                return new UnitOfWork(
                    db,
                 new ProductRepository(db.Set<Product>()),
            new CategoryRepository(db.Set<Category>())
                );
            });

            return services;
        }
    }

}
