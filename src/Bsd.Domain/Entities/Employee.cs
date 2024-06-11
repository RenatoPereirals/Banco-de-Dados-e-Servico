using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class Employee
    {
        public Employee()
        {
            BsdEntities = new HashSet<BsdEntity>();
        }

        [Key]
        public int EmployeeId { get; set; }
        public int Digit { get; set; }
        public ServiceType ServiceType { get; set; }
        public DateTime DateService { get; set; }
        public ICollection<BsdEntity> BsdEntities { get; set; }
    }
}
