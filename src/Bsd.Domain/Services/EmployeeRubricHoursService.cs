using Bsd.Domain.Entities;

namespace Bsd.Domain.Services
{
    public class EmployeeRubricHoursService
    {
       public EmployeeRubricHours CreateEmployeeRubricHours(BsdEntity bsdEntity, Employee employee, Rubric rubric)
       {
            var employeeRubricHours = new EmployeeRubricHours()
            {
                EmployeeRegistration = employee.Registration,
                EmployeeDigit = employee.Digit,
                RubricCode = rubric.Code
            };
            return employeeRubricHours;
       } 
    }
}