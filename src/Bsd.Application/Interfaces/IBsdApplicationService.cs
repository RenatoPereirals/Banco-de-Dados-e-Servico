using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces
{
    public interface IBsdApplicationService
    {
        Task<IEnumerable<EmployeeRubricHours>> GetBsdEntitiesDtoByDateRangeAsync(string startDate, string endDate);
        Task<bool> CreateBsdAsync(CreateBsdRequest request);
    }
}