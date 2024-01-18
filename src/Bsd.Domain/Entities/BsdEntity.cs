using Bsd.Domain.Enums;
namespace Bsd.Domain.Entities
{
public class BsdEntity
{
    public int BsdNumber { get; set; }
    public List<int> EmployeeIds { get; set; }
    public List<Employee> Employees { get; set; } 
    public DateTime DateService { get; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DayType DayType { get; }
    public Dictionary<int, List<Rubric>> EmployeeRubrics { get; set; } = new();

    public BsdEntity(int bsdNumber,
            List<int> employeeIds,
            DateTime dateService)
    {
        BsdNumber = bsdNumber;
        DateService = dateService;
        EmployeeIds = employeeIds;
    }
}

}
