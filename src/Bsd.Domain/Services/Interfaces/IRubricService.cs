
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces;

public interface IRubricService
{
    Task AssociateRubricsToEmployeeAsync(BsdEntity bsd);
    Task<ICollection<Rubric>> GetAllowedRubric(ServiceType serviceType, DayType dayType);
}