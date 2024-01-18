using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using Moq;

namespace test.Domain.Services
{
    public class BsdServiceTest
    {
        private readonly Employee employee;
        private readonly BsdEntity bsd;
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private readonly Mock<IRubricRepository> _rubricRepository;

        public BsdServiceTest()
        {
            // Arrange
            employee = new Employee(1234, ServiceType.P140);
            bsd = new BsdEntity(54321, new List<int>(), DateTime.Now);
            _employeeRepository = new Mock<IEmployeeRepository>();
            _rubricRepository = new Mock<IRubricRepository>();
        }

        public async Task<Dictionary<int, List<Rubric>>>  SetupAndAct(List<Rubric> listRubrics)
        {
            // Arrange
            _rubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(listRubrics);

            // Act
            var bsdService = new BsdService(_employeeRepository.Object, _rubricRepository.Object);
            var result = await bsdService.FilterRubricsBasedOnTheEmployeeTypeServiceAndTypeDay(bsd);

            return result;
        }

        [Fact]
        public async void Should_Return_Correct_Number_Of_Rubrics_For_Each_Day_Type()
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
            foreach (var employeeId in bsd.EmployeeIds)
            {
                Assert.Equal(2, result[employeeId].Count(r => r.ServiceType == ServiceType.P140));
                Assert.Equal(1, result[employeeId].Count(r => r.ServiceType == ServiceType.P110));
            }
        }

        [Fact]
        public async void CalculateOvertimeHoursList_Filter_Rubrics_Based_On_The_Bsd_Day_Type()
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
            Assert.NotNull(result);
            foreach (var employeeId in bsd.EmployeeIds)
            {
                Assert.Equal(2, result[employeeId].Count(r => r.DayType == DayType.Sunday));
                Assert.Equal(1, result[employeeId].Count(r => r.DayType == DayType.HoliDay));
                Assert.Equal(1, result[employeeId].Count(r => r.DayType == DayType.Workday));
            }
        }
    }
}
