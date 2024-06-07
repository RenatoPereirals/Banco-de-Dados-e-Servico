namespace Bsd.Application.Helpers.Interfaces
{
    public interface IEmployeeValidationService
    {
        Task<bool> ValidateEmployeeRegistrationsAsync(List<int> employeeRegistrations);
    }
}