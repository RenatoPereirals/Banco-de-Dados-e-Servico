﻿using Bsd.Domain.Entities;
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
            var employeeList = new List<EmployeeBsdEntity>();

            // Act
            var bsd = new BsdEntity(bsdNumber, dateService);

            // Assert
            Assert.Equal(bsdNumber, bsd.BsdNumber);
            Assert.Equal(dateService, bsd.DateService);
            Assert.Equal(employeeList, bsd.EmployeeBsdEntities);
        }
    }
}
