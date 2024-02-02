namespace Bsd.Domain.Services.Interfaces
{
    public interface IEmployeeService
    {
        void ValidateRegistration(int registration);
        int CalculateModulo11CheckDigit(int registration);
    }
}