using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.RepositoryImpl;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Domain.Persistence.RepositoryImpl
{
    public class RubricRepository : GeralRepository, IRubricRepository
    {
        private readonly BsdDbContext _context;
        public RubricRepository(BsdDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rubric>> GetAllRubricsAsync()
        {
            return await _context.Rubrics.ToListAsync();
        }
    }
}