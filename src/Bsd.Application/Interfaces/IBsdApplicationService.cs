using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IBsdApplicationService
    {
        Task<IEnumerable<BsdEntityDto>> GetBsdEntitiesDtoByDateRangeAsync(string startDate, string endDate);
        Task CreateBsdAsync(int bsdNumber, string dateService, int employeeRegistration, int digit);
    }
}