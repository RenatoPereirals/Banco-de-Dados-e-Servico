using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public int BsdNumber { get; set; }
        public DateTime DateService { get; set; }
        public DayType DayType { get; set; }
        public ICollection<EmployeeBsdEntity> EmployeeBsdEntities { get; set; } = new List<EmployeeBsdEntity>();
    }
}
