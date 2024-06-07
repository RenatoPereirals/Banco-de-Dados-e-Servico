using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Bsd.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Infrastructure.RepositoryImpl
{
    public class BsdRepository : GeralRepository, IBsdRepository
    {
        private readonly BsdDbContext _context;
        private readonly IGeralRepository _geralRepository;
        private readonly IDayTypeChecker _dayTypeChecker;
        public BsdRepository(BsdDbContext context,
                             IGeralRepository geralRepository,
                             IDayTypeChecker dayTypeChecker) : base(context)
        {
            _context = context;
            _geralRepository = geralRepository;
            _dayTypeChecker = dayTypeChecker;
        }

        public async Task<bool> CreateBsdAsync(BsdEntity bsd)
        {
            try
            {
                var day = _dayTypeChecker.GetDayType(bsd.DateService);

                ICollection<Employee> employees = new List<Employee>();
                foreach (var employee in bsd.Employees)
                {
                    employees.Add(employee);
                }

                var bsdEntity = new BsdEntity
                {
                    BsdId = bsd.BsdId,
                    DateService = bsd.DateService,
                    DayType = day,
                    Employees = employees,
                    Rubrics = bsd.Rubrics
                };

                _geralRepository.Create(bsdEntity);
                return await _geralRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new DbUpdateException($"Erro ao tentar criar BSD.");
            }
            catch (Exception)
            {
                throw new Exception("Erro interno inesperado, tente novamente.");
            }
        }

        public async Task<IEnumerable<BsdEntity>> GetBsdEntitiesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await GetBsdQuery()
               .Where(b => b.DateService.Date >= startDate.Date && b.DateService.Date <= endDate.Date)
               .ToListAsync();
        }

        public async Task<IEnumerable<BsdEntity>> GetAllBsdAsync()
        {
            return await GetBsdQuery().ToListAsync();
        }

        public async Task<BsdEntity> GetBsdByIdAsync(int bsdId)
        {
            return await GetBsdQuery()
                .OrderBy(b => b.BsdId)
                .FirstOrDefaultAsync(b => b.BsdId == bsdId)
                ?? throw new InvalidOperationException($"Bsd com o número {bsdId} não encontrado.");
        }

        private IQueryable<BsdEntity> GetBsdQuery()
        {
            return _context.BsdEntities
                .Include(eb => eb.Employees)
                .AsNoTracking();
        }

        public Task<BsdEntity> AddEmployeeToBsdAsync(int bsdId, int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
