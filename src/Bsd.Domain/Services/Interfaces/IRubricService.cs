using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IRubricService
    {
        Task<List<EmployeeRubricHours>> GetEmployeeRubricHoursAsync(DateTime startDate, DateTime endDate);
    }
}