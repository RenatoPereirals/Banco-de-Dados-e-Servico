using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services
{
    public class CalculateRubricHours : ICalculateRubricHours
    {
        private readonly IHolidayChecker _holidayChecker;

        public CalculateRubricHours(IHolidayChecker holidayChecker)
        {
            _holidayChecker = holidayChecker;
        }

        public void CalculateTotalWorkedHours(BsdEntity bsdEntity)
        {
            if (bsdEntity.Employees == null || !bsdEntity.Employees.Any())
                throw new InvalidOperationException("No employees found in BSD entity.");

            foreach (var employee in bsdEntity.Employees)
            {
                var hoursPerRubric = new Dictionary<int, decimal>();

                Console.WriteLine($"Funcionário: {employee.EmployeeId} - Tipo de serviço: {employee.ServiceType}");
                foreach (var rubric in employee.Rubrics)
                {
                    if (rubric.RubricId == 1935)
                        SetRubricTimesByServiceType(rubric, employee.ServiceType);

                    if (rubric.RubricId == 1932)
                        SetRubricTimesByHolidayEve(rubric, employee.WorkedDays);

                    if (rubric.RubricId == 1935)
                        Console.WriteLine($"RubricId: {rubric.RubricId} - HoursPerDay: {rubric.HoursPerDay}");

                    if (hoursPerRubric.ContainsKey(rubric.RubricId))
                        hoursPerRubric[rubric.RubricId] += rubric.HoursPerDay;
                    else
                        hoursPerRubric[rubric.RubricId] = rubric.HoursPerDay;
                }

                var newRubrics = hoursPerRubric.Select(entry => new Rubric
                {
                    RubricId = entry.Key,
                    TotalWorkedHours = entry.Value,
                    HoursPerDay = employee.Rubrics.First(r => r.RubricId == entry.Key).HoursPerDay,
                    DayType = employee.Rubrics.First(r => r.RubricId == entry.Key).DayType,
                    ServiceType = employee.Rubrics.First(r => r.RubricId == entry.Key).ServiceType
                }).ToList();

                employee.Rubrics = newRubrics;
                hoursPerRubric.Clear();
            }
        }

        private void SetRubricTimesByServiceType(Rubric rubric, ServiceType serviceType)
        {
            if (serviceType == ServiceType.P110)
                rubric.HoursPerDay = 12;
            else if (serviceType == ServiceType.P140)
                rubric.HoursPerDay = 11;
        }
            
        private void SetRubricTimesByHolidayEve(Rubric rubric, ICollection<WorkedDay> workedDays)
        {
            foreach (var workedDay in workedDays)
            {
                if (rubric.RubricId == 1932)
                {
                    if (_holidayChecker.IsHolidayEve(workedDay.DateEntry))
                        rubric.HoursPerDay = 6;
                    else
                        rubric.HoursPerDay = 18;
                }
            }
        }
    }
}
