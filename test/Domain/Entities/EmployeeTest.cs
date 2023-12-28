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
            Employee e = new Employee(3782, TypeService.P140);

            // Act
            var typeServiceString = e.TypeService.ToString();

            // Assert
            Assert.Equal("P140", typeServiceString);
            Assert.Equal(6, e.Digit);
            Assert.True(e.Bsd);
        }
    }
}