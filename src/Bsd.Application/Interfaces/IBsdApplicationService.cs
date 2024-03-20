using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces
{
    public interface IBsdApplicationService
    {
        Task<IEnumerable<EmployeeRubricHours>> GetBsdEntitiesDtoByDateRangeAsync(string startDate, string endDate);
        Task CreateBsdAsync(int bsdNumber, string dateService, int employeeRegistration, int digit);
    }
}