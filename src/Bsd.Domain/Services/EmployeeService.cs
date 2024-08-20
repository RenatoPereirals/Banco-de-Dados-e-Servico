using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IStaticDataService _staticDataService;

    public EmployeeService(IStaticDataService staticDataService)
    {
        _staticDataService = staticDataService;
    }

    public async Task<BsdEntity> AssociateEmployeesToBsdAsync(ICollection<Employee> employees)
    {
        BsdEntity bsd = new();

        var employeeIds = employees.Select(e => e.EmployeeId).Distinct().ToList();

        var employeeTasks = employeeIds.Select(id => _staticDataService.GetEmployeeById(id));
        var employeeResults = await Task.WhenAll(employeeTasks);
        var employeeDictionary = employeeIds.Zip(employeeResults, (id, employee) => new { Id = id, Employee = employee })
            .ToDictionary(x => x.Id, x => x.Employee);

        bsd.Employees = employees
            .Where(e => employeeDictionary.ContainsKey(e.EmployeeId))
            .Select(e => employeeDictionary[e.EmployeeId])
            .ToList();

        return bsd;
    }

    public async Task<ICollection<Employee>> GetEmployeesByRegistrationIdsAsync(ICollection<int> registrationIds)
    {
        var employeeTasks = registrationIds.Select(id => _staticDataService.GetEmployeeById(id));
        var employeeResults = await Task.WhenAll(employeeTasks);
        var employees = employeeResults.ToList();
    
        return employees;
    }
}