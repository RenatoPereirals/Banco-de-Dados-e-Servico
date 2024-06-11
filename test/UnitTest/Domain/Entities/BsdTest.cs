using Bsd.Domain.Entities;
namespace test.Domain.Entities
{
    public class BsdTest
    {
        [Fact]
        public void BsdEntity_ShouldInitializePropertiesWithValues()
        {
            // Arrange
            var bsd = new BsdEntity();
            var expectedBsdNumber = 25123;
            var expectedDateService = DateTime.UtcNow;
            var expectedEmployeeList = new List<Employee>();

            // Act
            bsd.BsdId = expectedBsdNumber;
            bsd.DateService = expectedDateService;
            bsd.Employees = expectedEmployeeList;

            // Assert
            Assert.Equal(expectedBsdNumber, bsd.BsdId);
            Assert.Equal(expectedDateService, bsd.DateService);
            Assert.Same(expectedEmployeeList, bsd.Employees);
        }
    }
}
