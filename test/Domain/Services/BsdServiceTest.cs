using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using test.Domain.Services.TestSetup;

namespace test.Domain.Services
{
    public class BsdServiceTest
    {
        private readonly TestSetupBsdServiceTest _testSetup;

        public BsdServiceTest()
        {
            _testSetup = new TestSetupBsdServiceTest();
        }


        private void AssertRubrics(Dictionary<int, List<Rubric>> result, Func<Rubric, bool> predicate, int expectedCount)
        {
            // Assert
            Assert.NotNull(result);
            foreach (var employeeBsdEntity in _testSetup.Bsd.EmployeeBsdEntities)
            {
                var employeeId = employeeBsdEntity.EmployeeRegistration;
                Assert.Equal(expectedCount, result[employeeId].Count(predicate));
            }
        }


        private readonly List<Rubric> listRubrics = new()
        {
            new("1", "a", 3, DayType.Workday, ServiceType.P140),
            new("2", "b", 2, DayType.Sunday, ServiceType.P140),
            new("3", "b", 2, DayType.Sunday, ServiceType.P140),
            new("4", "c", 1, DayType.HoliDay, ServiceType.P110),
        };

        [Fact]
        public async void Should_Return_Correct_Number_Of_Rubrics_For_Each_Service_Type()
        {
            var result = await _testSetup.SetupAndAct(listRubrics);

            AssertRubrics(result, r => r.ServiceType == ServiceType.P140, 3);
            AssertRubrics(result, r => r.ServiceType == ServiceType.P110, 1);
        }

        [Fact]
        public async void Should_Return_Correct_Number_Of_Rubrics_For_Each_Day_Type()
        {
            var result = await _testSetup.SetupAndAct(listRubrics);

            AssertRubrics(result, r => r.DayType == DayType.Sunday, 2);
            AssertRubrics(result, r => r.DayType == DayType.HoliDay, 1);
            AssertRubrics(result, r => r.DayType == DayType.Workday, 1);
        }
    }
}
