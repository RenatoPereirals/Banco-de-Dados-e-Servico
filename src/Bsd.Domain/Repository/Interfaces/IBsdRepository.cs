using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository
    {
        Task<BsdEntity> GetBsdByIdAsync(int bsdId);
        Task<IEnumerable<BsdEntity>> GetAllBsdAsync();
        Task<IEnumerable<BsdEntity>> GetEmployeeBsdEntitiesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task CreateBsdAsync(int bsdNumber, DateTime dateService, int employeeRegistration, int digit);
    }
}