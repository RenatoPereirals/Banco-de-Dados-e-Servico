using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IRubricRepository
    {
        Task<IEnumerable<Rubric>> GetAllRubricsAsync();
        Task CreateRubricAsync(string code, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType);
    }
}