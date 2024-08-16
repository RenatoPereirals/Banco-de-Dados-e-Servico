
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IRubricService
    {
        Task AssociateRubricAsync(BsdEntity bsd);
        Task<ICollection<Rubric>> GetAllowedRubric(Employee employee, DayType dayType);
    }
}