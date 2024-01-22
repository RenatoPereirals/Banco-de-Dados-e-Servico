using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository : IGeralRepository<BsdEntity> 
    {
        Task<BsdEntity> GetBsdByIdAsync(int bsdId);
        Task<IEnumerable<BsdEntity>> GetAllBsdAsync();
    }
}