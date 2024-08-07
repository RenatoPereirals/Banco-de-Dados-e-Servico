using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CalculateEmployeeWorkedDays(int employeeRegistration, DateTime startDate, DateTime endDate);
        int CalculateModulo11CheckDigit(int registration);
    }
}