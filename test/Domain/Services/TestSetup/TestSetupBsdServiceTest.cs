using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using test.Domain.Services.TestDataBase;

using Moq;

namespace test.Domain.Services.TestSetup
{
    public class SetupBsdServiceTest : TestBase
    {
        public BsdEntity Bsd { get; private set; }
        public List<Rubric> ListRubrics { get; private set; }
        public Mock<IEmployeeRepository> EmployeeRepository { get; private set; }
        public Mock<IRubricService> RubricService { get; private set; }

        public SetupBsdServiceTest()
        {
            // Arrange
            Bsd = new BsdEntity(54321, DateTime.Now); // Supondo que a data seja um feriado
            EmployeeRepository = new Mock<IEmployeeRepository>();
            RubricService = new Mock<IRubricService>();
            ListRubrics = new List<Rubric>();

            SetupEmployees();
            SetupRubrics();
        }

        private void SetupEmployees()
        {
            // Adicione os funcionários ao bsd
            Bsd.EmployeeBsdEntities.Add(new EmployeeBsdEntity(1, new Employee(1234, ServiceType.P110), 54321, Bsd));
            Bsd.EmployeeBsdEntities.Add(new EmployeeBsdEntity(2, new Employee(2345, ServiceType.P140), 54321, Bsd));
            Bsd.EmployeeBsdEntities.Add(new EmployeeBsdEntity(3, new Employee(3456, ServiceType.P140), 54321, Bsd));
        }

        private void SetupRubrics()
        {
            // Crie uma lista de rubricas
            ListRubrics = TestRubrics;

            RubricService.Setup(r => r.GetRubricsByServiceTypeAndDayAsync(It.IsAny<ServiceType>(), It.IsAny<DayType>()))
                .Returns<ServiceType, DayType>((serviceType, dayType) => Task.FromResult(ListRubrics.Where(r => r.ServiceType == serviceType && r.DayType == dayType).ToList()));
        }
    }

}