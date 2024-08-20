using Bsd.Application.DTOs;
using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces
{
    public interface IMarkProcessor
    {
        ICollection<Employee> ProcessMarks(ICollection<MarkResponse> markResponses);
    }
}