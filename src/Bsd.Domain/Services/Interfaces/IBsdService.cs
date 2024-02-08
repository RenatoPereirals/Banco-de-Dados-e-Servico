using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IBsdService
    {
        Task<BsdEntity> CreateBsdAsync(int bsdNumber, DateTime dateService, List<int> employeeRegistrations);
    }
}