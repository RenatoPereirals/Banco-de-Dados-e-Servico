namespace Bsd.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> CalculateEmployeeWorkedDays(int employeeRegistration, DateTime startDate, DateTime endDate);
        void ValidateRegistration(int registration);
        int CalculateModulo11CheckDigit(int registration);
    }
}