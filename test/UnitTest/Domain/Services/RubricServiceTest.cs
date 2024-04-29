using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services;
using test.Domain.Services.TestDataBase;
using Bsd.Domain.Services.Interfaces;
using Moq;
using Bsd.Domain.Entities;

namespace test.Domain.Services
{
    public class RubricServiceTest : TestBase
    {
        private readonly Mock<IBsdRepository> _mockBsdRepository;
        private readonly Mock<IRubricRepository> _mockRubricRepository;
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly RubricService _rubricService;

        public RubricServiceTest()
        {
            _mockBsdRepository = new Mock<IBsdRepository>();
            _mockRubricRepository = new Mock<IRubricRepository>();
            _mockEmployeeService = new Mock<IEmployeeService>();
            _rubricService = new RubricService(_mockRubricRepository.Object, _mockBsdRepository.Object, _mockEmployeeService.Object);
        }

        [Fact]
        public async Task Should_Returns_Total_Rubrics_List_Correct_Rubrics()
        {
            // Arrange
            var allRubrics = TestRubricsList;
            var startDate = DateTime.Parse("01/01/2024");
            var endDate = DateTime.Parse("31/12/2024");

            _mockRubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(allRubrics);

            // Act
            var result = await _rubricService.GetEmployeeRubricHoursAsync(startDate, endDate);

            // Assert
            var expectedResult = 2;
            Assert.Equal(expectedResult, result.Count);
        }

        [Fact]
        public async Task Should_Rutrun_Lista_Total_Hours_Per_Month_By_Rubrics_Correctly()
        {
            // Arrange
            var startDate = new DateTime(2024, 1, 1);
            var endDate = new DateTime(2024, 12, 31);
            var testBsdEntitiesList = TestBsdList;

            var bsdEntitiesList = testBsdEntitiesList.SelectMany(eb => eb.EmployeeBsdEntities)
                                         .Select(eb => new BsdEntity {  });

            _mockBsdRepository.Setup(r => r.GetBsdEntitiesByDateRangeAsync(startDate, endDate))
                 .ReturnsAsync(bsdEntitiesList);

            var expectedRubricHours = TestEmployeeRubricHours;

            // Acta
            var result = await _rubricService.GetEmployeeRubricHoursAsync(startDate, endDate);

            // Assert
            Assert.Equal(expectedRubricHours.Count(), result.Count);
        }
    }
}
