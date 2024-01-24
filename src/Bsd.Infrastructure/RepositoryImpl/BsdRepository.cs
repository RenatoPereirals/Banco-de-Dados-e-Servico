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

        public async Task<IEnumerable<BsdEntity>> GetAllBsdAsync()
        {
            return await GetBsdQuery().OrderBy(b => b.DateService).ToListAsync();
        }

        public async Task<BsdEntity> GetBsdByIdAsync(int bsdId)
        {
            return await GetBsdQuery()
                .OrderBy(b => b.DateService)
                .FirstOrDefaultAsync(b => b.BsdNumber == bsdId)
                ?? throw new InvalidOperationException($"Bsd com o número {bsdId} não encontrado.");
        }

        private IQueryable<BsdEntity> GetBsdQuery()
        {
            return _context.BsdEntities
                .Include(b => b.EmployeeBsdEntities)
                .ThenInclude(eb => eb.Employee)
                .AsNoTracking();
        }
    }
}
