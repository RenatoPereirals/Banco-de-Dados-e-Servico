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
            var bsdNumber = "BSD123";
            var dateService = DateTime.Now;
            var employeeList = new List<Employee>();

            // Act
            var bsd = new Bsd.Domain.Entities.Bsd(bsdNumber, employeeList, dateService);

            // Assert
            Assert.Equal(bsdNumber, bsd.BsdNumber);
            Assert.Equal(dateService, bsd.DateService);
            Assert.Equal(employeeList, bsd.Employee);
        }
    }
}
