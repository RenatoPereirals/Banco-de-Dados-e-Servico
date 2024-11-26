using Bsd.Domain.Entities;

namespace Bsd.Domain.Tests.Entities
{
    public class BsdEntityTests
    {
        [Fact]
        public void BsdEntity_InitializeEmployees_CreatesEmptyList()
        {
            // Arrange
            var bsdEntity = new BsdEntity();

            // Act
            var employees = bsdEntity.Employees;

            // Assert
            Assert.NotNull(employees);
            Assert.IsType<List<Employee>>(employees);
            Assert.Empty(employees);
        }
    }
}