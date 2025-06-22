using Case.Shared.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Infrastructure.Persistance
{
    public class CaseDbContextFactory : IDesignTimeDbContextFactory<CaseAPIDbContext>
    {
        public CaseAPIDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CaseAPIDbContext>();

            var basePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Case.API"));
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("CaseAPIDb");
            optionsBuilder.UseSqlServer(connectionString);

            return new CaseAPIDbContext(optionsBuilder.Options);
        }
    }
}
