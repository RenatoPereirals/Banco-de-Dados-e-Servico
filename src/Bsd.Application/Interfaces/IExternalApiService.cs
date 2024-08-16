using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IExternalApiService
    {
        Task<ICollection<MarkResponse>> GetMarkAsync(MarkRequest request);
    }
}