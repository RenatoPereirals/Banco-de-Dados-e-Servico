using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Services
{
    public class CalculateRubricHours : ICalculateRubricHours
    {
        public void CalculateTotalWorkedHours(BsdEntity bsdEntity)
        {
            if (bsdEntity.Employees == null || !bsdEntity.Employees.Any())
                throw new InvalidOperationException("No employees found in BSD entity.");

            foreach (var employee in bsdEntity.Employees)
            {
                var hoursPerRubric = new Dictionary<int, decimal>();

                foreach (var rubric in employee.Rubrics)
                {
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
            }
        }
    }
}
