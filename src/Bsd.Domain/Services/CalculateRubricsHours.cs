using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Domain.Services
{
    public class CalculateRubricHours : ICalculateRubricHours
    {
        public void CalculateTotalWorkedHours(IEnumerable<BsdEntity> bsdEntities)
        {
            var rubricAssignments = bsdEntities.SelectMany(bsd => bsd.Employees);
            var allowedRubrics = rubricAssignments.SelectMany(r => r.Rubrics);

            var groupedRubrics = allowedRubrics.GroupBy(r => r.RubricId);

            foreach (var group in groupedRubrics)
            {
                var totalHours = group.Sum(r => r.HoursPerDay);

                foreach (var rubric in group)
                {
                    rubric.TotalWorkedHours = totalHours;
                }
            }
        }
    }
}