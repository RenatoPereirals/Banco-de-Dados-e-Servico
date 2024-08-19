using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CalculateEmployeeWorkedDays(int employeeRegistration, DateTime startDate, DateTime endDate);
        Task AssociateEmployeesToBsdAsync(ICollection<BsdEntity> bsdEntities, List<int> employeesIds);
        int CalculateModulo11CheckDigit(int registration);
    }
}