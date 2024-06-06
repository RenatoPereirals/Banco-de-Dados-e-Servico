using Bsd.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Infrastructure.Context
{
    public class BsdDbContext : DbContext
    {
#pragma warning disable CS8618

        public BsdDbContext(DbContextOptions<BsdDbContext> options) : base(options) { }

        public DbSet<BsdEntity> BsdEntities { get; set; }
        public DbSet<Employee> Employees { get; set; }

#pragma warning restore CS8618
    }
}