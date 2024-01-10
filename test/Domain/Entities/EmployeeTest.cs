using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Moq;

namespace test.Domain.Entities
{
    public class EmployeeTest
    {
        [Fact]
        public void EmployeeConstructor_ShouldCreateInstance()
        {
            // Arrange
            var mockRubric = new Mock<Rubric>();

            mockRubric.Setup(r => r.Code).Returns("Valor1");
            var listRubrics = new List<Rubric> { mockRubric.Object };

            Employee employee = new("3782", ServiceType.P140, listRubrics);

            // Act
            var typeServiceString = employee.ServiceType.ToString();

            // Assert
            Assert.Equal("P140", typeServiceString);
            Assert.Equal(6, employee.Digit);
            Assert.NotNull(employee.Bsd);
            Assert.Single(employee.Rubrics); 
        }
    }
}