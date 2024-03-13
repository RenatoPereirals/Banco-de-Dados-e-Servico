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
        public async Task<IEnumerable<Rubric>> GetAllRubricsAsync()
        {
            return await _context.Rubrics.ToListAsync();
        }

        public async Task CreateRubricAsync(string code, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType)
        {
            var rubric = new Rubric
            {
                Code = code,
                Description = description,
                HoursPerDay = hoursPerDay,
                DayType = dayType,
                ServiceType = serviceType
            };

            _geralRepository.Create(rubric);
            await _geralRepository.SaveChangesAsync();
        }
    }
}