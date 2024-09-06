using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces;

public interface IEmployeeService
{
    Task<ICollection<Employee>> GetEmployeesByRegistrationIdsAsync(ICollection<int> registrationIds);
    Task<BsdEntity> AssociateEmployeesToBsdAsync(ICollection<Employee> employees);
}