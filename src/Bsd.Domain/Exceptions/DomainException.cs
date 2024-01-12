using Bsd.Domain.Entities;
using Bsd.Domain.Exceptions.Interfaces;

namespace Bsd.Domain.Exceptions
{
    public class DomainException : IEmployeeException
    {
        public void ValidateEmployeeIdNotNull(Employee employee)
        {            
            if (employee== null)
            {
                throw new ArgumentNullException(nameof(employee), "Funcionário não pode ser nulo");
            }
        }
    }
}