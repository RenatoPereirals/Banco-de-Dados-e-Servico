namespace Bsd.Domain.Entities
{
    public class BsdEntity
    {
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
