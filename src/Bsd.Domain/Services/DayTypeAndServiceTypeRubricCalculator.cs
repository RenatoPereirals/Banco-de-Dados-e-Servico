using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Service.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class DayTypeAndServiceTypeRubricCalculator : IDayTypeAndServiceTypeRubricCalculator
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHoliDayChecker _holiDayChecker;

        public DayTypeAndServiceTypeRubricCalculator(IEmployeeRepository employeeRepository,
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
            var dayTypeRubrics = new List<Rubric>(employee.Rubrics);

            if (dayType == DayType.Sunday)
            {
                var sundayRubrics = new List<Rubric>
                {
                    new("1937", "se domingos então 7h/dia de 100%", 7.00m, DayType.Sunday, ServiceType.P140),
                    new("1921", "se domingos ou feriados então 3h/dia de 150%", 3.00m, DayType.Sunday, ServiceType.P140)
                };

                dayTypeRubrics.AddRange(sundayRubrics);
            }

            return dayTypeRubrics;
        }

        public async Task<List<Rubric>> CalculateOvertimeRubricsBasedOnServiceType(string employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(employeeId);
            var serviceTypeRubrics = new List<Rubric>();
            
            if(employee.ServiceType == ServiceType.P140)
            {
                var P140Rubrics = new List<Rubric>
                {
                    new("1937", "se domingos então 7h/dia de 100%", 7.00m, DayType.Sunday, ServiceType.P140),
                    new("1921", "se domingos ou feriados então 3h/dia de 150%", 3.00m, DayType.Sunday, ServiceType.P140)
                };

                serviceTypeRubrics.AddRange(P140Rubrics);
            } 
            return serviceTypeRubrics;
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