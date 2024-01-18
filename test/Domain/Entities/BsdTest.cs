using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Moq;
namespace test.Domain.Entities
{
    public class BsdTest
    {        

        [Fact]
        public void BsdConstructor_ShouldCreateInstance()
        {
            // Arrange
            var bsdNumber = 25123;
            var dateService = DateTime.Now;
            var employeeList = new List<int>();

            // Act
            var bsd = new BsdEntity(bsdNumber, employeeList, dateService);

            // Assert
            Assert.Equal(bsdNumber, bsd.BsdNumber);
            Assert.Equal(dateService, bsd.DateService);
            Assert.Equal(employeeList, bsd.EmployeeIds);
        }
    }
}
