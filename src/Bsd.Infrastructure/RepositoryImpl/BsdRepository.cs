using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;

using Bsd.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;

namespace Bsd.Infrastructure.RepositoryImpl
{
    public class BsdRepository : GeralRepository, IBsdRepository
    {
        private readonly BsdDbContext _context;
        public BsdRepository(BsdDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateBsdAsync(BsdEntity bsd)
        {
            _context.Add(bsd);
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<ICollection<BsdEntity>> GetBsdEntitiesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetBsdQuery()
               .Where(b => b.DateService.Date >= startDate.Date && b.DateService.Date <= endDate.Date)
               .ToListAsync();
        }

        public async Task<IEnumerable<BsdEntity>> GetAllBsdAsync()
        {
            return await GetBsdQuery().ToListAsync();
        }

        private IQueryable<BsdEntity> GetBsdQuery()
        {
            return _context.BsdEntities
                .Include(eb => eb.Employees)
                .AsNoTracking();
        }

        public async Task<ICollection<Employee>> AddEmployeeToBsdAsync(BsdEntity bsd)
        {
            var employees = bsd.Employees.Where(employee => employee != null).ToList();

            return await Task.FromResult(employees);
        }
    }
}
