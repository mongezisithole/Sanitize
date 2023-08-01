using Microsoft.EntityFrameworkCore;
using Sanitize.Core;

namespace Sanitize.Data.Context
{
    public class SensitiveContext : DbContext
    {
        public SensitiveContext(DbContextOptions<SensitiveContext> options)
        : base(options: options) { }

        public DbSet<SensitiveWord> SensitiveWords { get; set; }
    }
}
