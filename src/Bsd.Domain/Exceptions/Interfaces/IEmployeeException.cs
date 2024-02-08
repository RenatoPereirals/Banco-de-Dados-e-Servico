using Bsd.Domain.Entities;

namespace Bsd.Domain.Exceptions.Interfaces
{
    public interface IEmployeeException
    {
        void ValidateEmployeeIdNotNull(Employee employeeId);
    }
}