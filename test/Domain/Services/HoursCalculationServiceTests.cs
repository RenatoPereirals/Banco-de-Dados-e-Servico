using System.Reflection;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Services;
using Bsd.Domain.Services.Interfaces;
using Moq;

namespace test.Domain.Services
{
    public class HoursCalculationServiceTests
    {
        private readonly Employee employee;
        private readonly BsdEntity bsd;
        private readonly Mock<IHoursCalculationService> mockService;

        public HoursCalculationServiceTests()
        {
            // Arrange
            employee = new Employee("1230", ServiceType.P140, new List<Rubric>());
            bsd = new BsdEntity("12365", new List<Employee>(), DateTime.Now);
            mockService = new Mock<IHoursCalculationService>();
        }

        private async Task<List<Rubric>> SetupAndAct(List<Rubric> listRubrics)
        {
            mockService.Setup(service => service.CalculateOvertimeHoursList(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(listRubrics); // Retorna a lista de rubricas

            // Act
            return await mockService.Object.CalculateOvertimeHoursList(employee.Registration, bsd.BsdNumber);
        }

        [Fact]
        public async void CalculateOvertimeHoursList_ShouldReturnQuantityOfRubricsForServiceType()
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
            Assert.Equal(2, result.Count(r => r.ServiceType == ServiceType.P140));
            Assert.Equal(1, result.Count(r => r.ServiceType == ServiceType.P110));
        }

        [Fact]
        public async void CalculateOvertimeHoursList_ShouldReturnQuantityOfRubricsForDayType()
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
            Assert.Equal(2, result.Count(r => r.DayType == DayType.Sunday));
            Assert.Equal(1, result.Count(r => r.DayType == DayType.HoliDay));
            Assert.Equal(1, result.Count(r => r.DayType == DayType.Workday));
        }
    }
}
