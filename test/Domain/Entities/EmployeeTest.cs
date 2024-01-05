using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Entities
{
    public class EmployeeTest
    {
        

        [Fact]
        public void EmployeeInitializationTest()
        {
            // Arrange
            Employee e = new Employee("3782", ServiceType.P140);

            // Act
            var typeServiceString = e.ServiceType.ToString();

            // Assert
            Assert.Equal("P140", typeServiceString);
            Assert.Equal(6, e.Digit);
            Assert.NotNull(e.Bsd);
        }
    }
}