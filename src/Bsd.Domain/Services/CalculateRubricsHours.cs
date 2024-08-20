using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services;
public class CalculateRubricHours : ICalculateRubricHours
{
    public void CalculateTotalWorkedHours(BsdEntity bsdEntity)
    {
        var worker = bsdEntity.Employees.FirstOrDefault();

        if (worker != null)
        {
            var rubrics = worker.Rubrics;

            var totalHoursByRubric = new Dictionary<int, decimal>();

            foreach (var rubric in rubrics)
            {
                if (totalHoursByRubric.ContainsKey(rubric.RubricId))
                {
                    totalHoursByRubric[rubric.RubricId] += rubric.HoursPerDay;
                }
                else
                {
                    totalHoursByRubric[rubric.RubricId] = rubric.HoursPerDay;
                }
            }

            foreach (var rubric in rubrics)
            {
                rubric.TotalWorkedHours = totalHoursByRubric[rubric.RubricId];
            }
        }
        else
        {
            throw new InvalidOperationException($"No employees found in BSD entity.");
        }
    }
}


