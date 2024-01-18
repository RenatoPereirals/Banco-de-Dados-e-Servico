using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository : ICreatableUpdatable<BsdEntity> 
    {
        Task<BsdEntity> GetBsdByIdAsync(int bsdId);
    }
}