using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository
    {
        Task<BsdEntity> GetBsdByIdAsync(int bsdId);
        Task<IEnumerable<BsdEntity>> GetAllBsdAsync();
        Task<IEnumerable<BsdEntity>> GetBsdEntitiesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ICollection<Employee>> AddEmployeeToBsdAsync(BsdEntity bsd);
        Task<bool> CreateBsdAsync(BsdEntity bsd);
    }
}