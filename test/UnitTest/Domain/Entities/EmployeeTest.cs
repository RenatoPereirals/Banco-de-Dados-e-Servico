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
            var bsdList = new List<EmployeeBsdEntity>();
            Employee employee = new(1525, ServiceType.P140);

            // Act
            var typeServiceString = employee.ServiceType.ToString();

            // Assert
            Assert.Equal("P140", typeServiceString);
            Assert.Equal(bsdList, employee.EmployeeBsdEntities);
        }
    }
}