using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IBsdService
    {
        Task<BsdEntity> CreateBsdAsync(int bsdNumber, DateTime dateService, IEnumerable<int> employeeRegistrations);
        Task<Dictionary<int, List<Rubric>>> AssignRubricsToEmployeesByServiceTypeAndDayAsync(BsdEntity bsd);
    }
}