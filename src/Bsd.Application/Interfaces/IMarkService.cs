using Bsd.Application.DTOs;

using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces
{
    public interface IMarkService
    {
        Task<BsdEntity> ProcessMarksAsync(ICollection<MarkResponse> marks);
    }
}