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
            var expectedEmployeeList = new List<EmployeeBsdEntity>();

            // Act
            bsd.BsdNumber = expectedBsdNumber;
            bsd.DateService = expectedDateService;
            bsd.EmployeeBsdEntities = expectedEmployeeList;

            // Assert
            Assert.Equal(expectedBsdNumber, bsd.BsdNumber);
            Assert.Equal(expectedDateService, bsd.DateService);
            Assert.Same(expectedEmployeeList, bsd.EmployeeBsdEntities);
        }
    }
}
