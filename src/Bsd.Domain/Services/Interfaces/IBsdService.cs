using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IBsdService
    {
        Task<ICollection<EmployeeRubric>> AssociateRubricsToEmployeesAsync(BsdEntity bsd, DayType day);
    }
}