using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository
    {
        Task<BsdEntity> GetBsdByIdAsync(int bsdId);
        Task<IEnumerable<BsdEntity>> GetAllBsdAsync();
    }
}