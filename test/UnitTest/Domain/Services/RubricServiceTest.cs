// using Bsd.Domain.Repository.Interfaces;
// using Bsd.Domain.Services;
// using test.Domain.Services.TestDataBase;
// using Bsd.Domain.Services.Interfaces;
// using Moq;
// using Bsd.Domain.Entities;

// namespace test.Domain.Services
// {
//     public class RubricServiceTest : TestBase
//     {
//         private readonly Mock<IBsdRepository> _mockBsdRepository;
//         private readonly Mock<IRubricRepository> _mockRubricRepository;
//         private readonly Mock<IEmployeeService> _mockEmployeeService;
//         private readonly RubricService _rubricService;

//         public RubricServiceTest()
//         {
//             _mockBsdRepository = new Mock<IBsdRepository>();
//             _mockRubricRepository = new Mock<IRubricRepository>();
//             _mockEmployeeService = new Mock<IEmployeeService>();
//             _rubricService = new RubricService(_mockRubricRepository.Object, _mockBsdRepository.Object, _mockEmployeeService.Object);
//         }

//         [Fact]
//         public async Task Should_Returns_Total_Rubrics_List_Correct_Rubrics()
//         {
//             // Arrange
//             var allRubrics = TestRubricsList;
//             var startDate = new DateTime(2024, 1, 1);
//             var endDate = new DateTime(2024, 12, 31);

//             _mockBsdRepository.Setup(b => b.GetBsdEntitiesByDateRangeAsync(startDate, endDate)).ReturnsAsync(TestBsdList);
//             _mockRubricRepository.Setup(r => r.GetAllRubricsAsync()).ReturnsAsync(allRubrics);
//             _mockEmployeeService.Setup(s => s.CalculateEmployeeWorkedDays(1234, startDate, endDate)).ReturnsAsync(7);

//             // Act
//             var result = await _rubricService.GetEmployeeRubricHoursAsync(startDate, endDate);

//             // Assert
//             var expectedResult = 7;
//             Assert.Equal(expectedResult, result.Count);
//         }
//     }
// }
