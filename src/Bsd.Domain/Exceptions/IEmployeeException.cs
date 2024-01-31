using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Exceptions.Interfaces
{
    public interface IEmployeeException
    {
        void ValidateEmployeeIdNotNull(Employee employeeId);
    }
}