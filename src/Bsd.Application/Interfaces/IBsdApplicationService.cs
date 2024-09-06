using Bsd.Domain.Entities;

namespace Bsd.Application.Interfaces;

public interface IBsdApplicationService
{
    Task<BsdEntity> CreatebsdEntityAsync(ICollection<Employee> employees);
}