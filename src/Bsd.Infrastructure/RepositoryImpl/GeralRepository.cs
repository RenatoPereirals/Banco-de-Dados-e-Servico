using Bsd.Domain.Repository.Interfaces;
using Bsd.Infrastructure.Context;

namespace Bsd.Infrastructure.RepositoryImpl
{
    public class GeralRepository : IGeralRepository
    {
        private readonly BsdDbContext _context;

        public GeralRepository(BsdDbContext context)
        {
            _context = context;
        }
        public void Create<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entities) where T : class
        {
            _context.RemoveRange(entities);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}