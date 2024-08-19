using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository
    {
        Task<IEnumerable<BsdEntity>> GetAllBsdAsync();
        Task<ICollection<BsdEntity>> GetBsdEntitiesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ICollection<Employee>> AddEmployeeToBsdAsync(BsdEntity bsd);
        Task<bool> CreateBsdAsync(BsdEntity bsd);
    }
}