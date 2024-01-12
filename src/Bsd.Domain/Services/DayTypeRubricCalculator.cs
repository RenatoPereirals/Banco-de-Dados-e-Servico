using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class DayTypeRubricCalculator : IDayTypeRubricCalculator
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHoliDayChecker _holiDayChecker;
        
        public DayTypeRubricCalculator(IEmployeeRepository employeeRepository,
                                       IHoliDayChecker holiDayChecker)
        {
            _employeeRepository = employeeRepository;
            _holiDayChecker = holiDayChecker;
        }

        public async Task<List<Rubric>> CalculateOvertimeRubricsBasedOnDayType(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);
            var date = employee.DateService;
            var dayType = CalculateDayType(date);
            var rubrics = new List<Rubric>(employee.Rubrics);

            if (dayType == DayType.Sunday)
            {
                var sundayRubrics = new List<Rubric>
                {
                    new("1937", "se domingos então 7h/dia de 100%", 7.00m, DayType.Sunday, ServiceType.P140),
                    new("1921", "se domingos ou feriados então 3h/dia de 150%", 3.00m, DayType.Sunday, ServiceType.P140)
                };

                rubrics.AddRange(sundayRubrics);
            }

            return rubrics;
        }

        private DayType CalculateDayType(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Sunday)
                return DayType.Sunday;

            if (_holiDayChecker.IsHoliday(date))
                return DayType.HoliDay;

            return DayType.Workday;
        }
    }
}