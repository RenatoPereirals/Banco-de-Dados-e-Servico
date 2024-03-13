using Bsd.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Bsd.Infrastructure.Context
{
    public class BsdDbContext : DbContext
    {
#pragma warning disable CS8618

        public BsdDbContext(DbContextOptions<BsdDbContext> options) : base(options) { }

        public DbSet<BsdEntity> BsdEntities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeBsdEntity> EmployeesBsdEntities { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }

#pragma warning restore CS8618
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeBsdEntity>()
                        .HasKey(eb => new { eb.EmployeeRegistration, eb.BsdNumber });

            modelBuilder.Entity<BsdEntity>()
                        .HasKey(b => b.BsdNumber);

            modelBuilder.Entity<Employee>()
                        .HasKey(e => e.Registration);

            modelBuilder.Entity<Rubric>()
                        .HasKey(r => r.Code);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            }
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
