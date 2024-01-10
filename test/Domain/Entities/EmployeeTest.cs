using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Entities
{
    public class EmployeeTest
    {
        [Fact]
        public void EmployeeConstructor_ShouldCreateInstance()
        {
            // Arrange
            Employee employee = new("3782", ServiceType.P140, new List<Rubric>());

            // Act
            var typeServiceString = employee.ServiceType.ToString();
            var rubricsList = new List<Rubric>();

            // Assert
            Assert.Equal("P140", typeServiceString);
            Assert.Equal(6, employee.Digit);
            Assert.Equal(rubricsList, employee.Rubrics);
        }
    }
}