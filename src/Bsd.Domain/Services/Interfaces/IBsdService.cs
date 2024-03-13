using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IBsdService
    {
        Task CreateBsdAsync(int bsdNumber, DateTime dateService, int employeeRegistrations, int digit);
    }
}