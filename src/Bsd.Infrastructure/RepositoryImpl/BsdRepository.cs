using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
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
        private readonly IEmployeeRepository _employeeRepository;
        public BsdRepository(BsdDbContext context,
                             IGeralRepository geralRepository,
                             IDayTypeChecker dayTypeChecker,
                             IEmployeeRepository employeeRepository) : base(context)
        {
            _context = context;
            _geralRepository = geralRepository;
            _dayTypeChecker = dayTypeChecker;
            _employeeRepository = employeeRepository;
        }

        public async Task CreateBsdAsync(int bsdNumber, DateTime dateService, int employeeRegistration, int digit)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeRegistration);

                var day = _dayTypeChecker.GetDayType(dateService);
                var bsdEntity = new BsdEntity
                {
                    BsdNumber = bsdNumber,
                    DateService = dateService,
                    DayType = day,
                };

                _geralRepository.Create(bsdEntity);
                await _geralRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception($"Erro ao tentar criar BSD. Error: {innerException!.Message}");
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
                .OrderBy(b => b.BsdNumber)
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
