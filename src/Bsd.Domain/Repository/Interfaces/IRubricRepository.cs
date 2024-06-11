using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IRubricRepository
    {
        Task<IEnumerable<Rubric>> GetAllRubricsAsync();
        Task<IEnumerable<Rubric>> GetRubricsByServiceTypeAndDayTypeAsync(ServiceType serviceType, DayType dayType);
        Task CreateRubricAsync(int rubricId, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType);
    }
}