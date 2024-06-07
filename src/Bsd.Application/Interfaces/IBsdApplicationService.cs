using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IBsdApplicationService
    {
        Task<CreateBsdRequest> CreateBsdAsync(CreateBsdRequest request);
    }
}