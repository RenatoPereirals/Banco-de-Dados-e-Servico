using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace test.Domain.Services.TestDataBase
{
    public class TestBase
    {
        protected List<Rubric> TestRubrics => new List<Rubric>
        {
            new("1", "a", 3.0M, DayType.HoliDay, ServiceType.P110),
            new("2", "b", 2.0M, DayType.HoliDay, ServiceType.P110),
            new("3", "c", 1.0M, DayType.Sunday, ServiceType.P140),
            new("4", "d", 1.0M, DayType.Workday, ServiceType.P140),
        };
    }
}