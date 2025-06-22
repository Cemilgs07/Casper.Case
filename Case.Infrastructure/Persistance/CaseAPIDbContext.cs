using Case.Domain.Entities;
using Case.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace Case.Infrastructure.Persistance
{
    public class CaseAPIDbContext : DbContext
    {

        public CaseAPIDbContext(DbContextOptions<CaseAPIDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
