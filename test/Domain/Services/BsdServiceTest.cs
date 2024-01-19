using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;

namespace test.Domain.Services
{
    public class BsdServiceTest
    {
        private readonly BsdEntity bsd;
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IRubricRepository> _rubricRepository;

        public BsdServiceTest()
        {
            // Arrange
            bsd = new BsdEntity(54321, DateTime.Now);
            _employeeRepository = new Mock<IEmployeeRepository>();
            _rubricRepository = new Mock<IRubricRepository>();
        }

        public async Task<Dictionary<int, List<Rubric>>> SetupAndAct(List<Rubric> listRubrics)
        {
            // Arrange
            _rubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(listRubrics);

            // Act
            var bsdService = new BsdService( _rubricRepository.Object, _employeeRepository.Object);
            var result = await bsdService.FilterRubricsByServiceTypeAndDayAsync(bsd);

            return result;
        }

        [Fact]
        public async void Should_Return_Correct_Number_Of_Rubrics_For_Each_Service_Type()
        {
            var listRubrics = new List<Rubric>
            {
                new("1", "a", 3, DayType.Workday, ServiceType.P140),
                new("2", "b", 2, DayType.Sunday, ServiceType.P140),
                new("3", "c", 1, DayType.HoliDay, ServiceType.P110),
            };

            var result = await SetupAndAct(listRubrics);

            // Assert
            Assert.NotNull(result);
            foreach (var employeeBsdEntity in bsd.EmployeeBsdEntities)
            {
                var employeeId = employeeBsdEntity.EmployeeRegistration;
                Assert.Equal(2, result[employeeId].Count(r => r.ServiceType == ServiceType.P140));
                Assert.Equal(1, result[employeeId].Count(r => r.ServiceType == ServiceType.P110));
            }
        }

        [Fact]
        public async void Should_Return_Correct_Number_Of_Rubrics_For_Each_Day_Type()
        {
            var listRubrics = new List<Rubric>
            {
                new("1", "a", 3, DayType.Workday, ServiceType.P140),
                new("2", "b", 2, DayType.Sunday, ServiceType.P140),
                new("3", "b", 2, DayType.Sunday, ServiceType.P140),
                new("4", "c", 1, DayType.HoliDay, ServiceType.P110),
            };

            var result = await SetupAndAct(listRubrics);

            // Assert
            foreach (var employeeBsdEntity in bsd.EmployeeBsdEntities)
            {
                var employeeId = employeeBsdEntity.EmployeeRegistration;
                Assert.Equal(2, result[employeeId].Count(r => r.DayType == DayType.Sunday));
                Assert.Equal(1, result[employeeId].Count(r => r.DayType == DayType.HoliDay));
                Assert.Equal(1, result[employeeId].Count(r => r.DayType == DayType.Workday));
            }
        }
    }
}
