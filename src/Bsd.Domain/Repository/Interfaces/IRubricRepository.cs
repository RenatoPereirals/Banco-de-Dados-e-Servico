using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IRubricRepository
    {
        Task<List<Rubric>> GetAllRubricsAsync();
        Task<Rubric[]> GetRubricsByServiceTypeAsync(ServiceType serviceType);
        Task<Rubric[]> GetRubricsByDayTypeAsync(DayType dayType);
    }
}