using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Sanitize.Data.Context
{
    public class SensitiveWordFactory : IDesignTimeDbContextFactory<SensitiveContext>
    {
        /// <summary>
        /// A factory for creating SensitiveContext instance
        /// </summary>
        /// <param name="args">command-line arguments for EF commands</param>
        /// <returns></returns>
        public SensitiveContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SensitiveContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Username=postgres;Password=postgres;Database=SensitiveWords;Port=5432;SSLMode=Prefer");
            //optionsBuilder.UseInMemoryDatabase(databaseName: "SensitiveWords");
            return new SensitiveContext(optionsBuilder.Options);
        }
    }
}
