using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
namespace test.Domain.Entities
{
    public class BsdTest
    {
        [Fact]
        public void BsdConstructor_ShouldCreateInstance()
        {
            // Arrange
            var bsdNumber = "BSD123";
            var dateService = DateTime.Now;
            var employee1 = new Employee("1234", ServiceType.P140);
            var employee2 = new Employee("5678", ServiceType.P110);
            var employeeList = new List<Employee> { employee1, employee2 };

            // Act
            var bsd = new Bsd.Domain.Entities.Bsd(bsdNumber, employeeList, dateService);

            // Assert
            Assert.Equal(bsdNumber, bsd.BsdNumber);
            Assert.Equal(dateService, bsd.DateService);
            Assert.Equal(employeeList, bsd.Employee);
        }
    }
}
