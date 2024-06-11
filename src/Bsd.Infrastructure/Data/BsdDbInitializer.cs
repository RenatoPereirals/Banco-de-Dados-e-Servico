using Bsd.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Infrastructure.Data
{
    public class BsdDbInitializer
    {
        private readonly BsdDbContext _context;
        private readonly EmployeeSeeder _employeeSeeder;

        public BsdDbInitializer(BsdDbContext context, EmployeeSeeder employeeSeeder)
        {
            _context = context;
            _employeeSeeder = employeeSeeder;
        }

        public void Initialize()
        {
            _employeeSeeder.Seed(_context, AddEntitiesIfNotExists);
            _context.SaveChanges();
        }

        public void AddEntitiesIfNotExists<T>(DbSet<T> dbSet, List<T> entities) where T : class
        {
            if (!dbSet.Any())
            {
                dbSet.AddRange(entities);
                _context.SaveChangesAsync();
            }
        }
    }
}
