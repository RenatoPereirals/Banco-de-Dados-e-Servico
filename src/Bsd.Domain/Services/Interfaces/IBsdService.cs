using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IBsdService
    {
        Task<BsdEntity> CreateBsdAsync(BsdEntity bsd);
        Task<ICollection<BsdEntity>> CreateOrUpdateBsdsAsync(ICollection<BsdEntity> bsds);
    }
}