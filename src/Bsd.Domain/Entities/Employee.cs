using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public int Digit { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
