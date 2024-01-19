namespace Bsd.Domain.Entities
{
    public class EmployeeBsdEntity
    {
        public EmployeeBsdEntity(int employeeRegistration,
                                 Employee employee,
                                 int bsdEntityNumber,
                                 BsdEntity bsdEntity)
        {
            EmployeeRegistration = employeeRegistration;
            Employee = employee;
            BsdEntityNumber = bsdEntityNumber;
            BsdEntity = bsdEntity;
        }

        public int EmployeeRegistration { get; set; }
        public Employee Employee { get; set; }

        public int BsdEntityNumber { get; set; }
        public BsdEntity BsdEntity { get; set; }
    }
}