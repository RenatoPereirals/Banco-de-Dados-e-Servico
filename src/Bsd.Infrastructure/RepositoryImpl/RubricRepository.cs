using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Infrastructure.Context;
using Bsd.Infrastructure.RepositoryImpl;
using Microsoft.EntityFrameworkCore;

namespace Bsd.Domain.Persistence.RepositoryImpl
{
    public class RubricRepository : GeralRepository, IRubricRepository
    {
        private readonly BsdDbContext _context;
        private readonly IGeralRepository _geralRepository;
        public RubricRepository(BsdDbContext context,
                                IGeralRepository geralRepository) : base(context)
        {
            _context = context;
            _geralRepository = geralRepository;
        }
        public Task<IEnumerable<Rubric>> GetAllRubricsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task CreateRubricAsync(int rubricId, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType)
        {
            try
            {

                var rubric = new Rubric
                {
                    RubricId = rubricId,
                    Description = description,
                    HoursPerDay = hoursPerDay,
                    DayType = dayType,
                    ServiceType = serviceType
                };

                _geralRepository.Create(rubric);
                await _geralRepository.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Erro ao tentar criar rubica. Error: {ex.InnerException}");
            }
        }

        public async Task<IEnumerable<Rubric>> GetRubricsByServiceTypeAndDayTypeAsync(ServiceType serviceType, DayType dayType)
        {
            var rubrics = await _context.Rubrics
                .Where(rubric => rubric.ServiceType == serviceType && rubric.DayType == dayType)
                .ToListAsync();

            return rubrics;
        }
    }
}