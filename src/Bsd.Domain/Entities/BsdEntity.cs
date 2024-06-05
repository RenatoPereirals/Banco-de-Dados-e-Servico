using System.ComponentModel.DataAnnotations;
using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public BsdEntity()
        {
            Employees = new HashSet<Employee>();
        }
        
        [Key]
        public int BsdNumber { get; set; }
        public DateTime DateService { get; set; }
        public DayType DayType { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
