using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Services
{
    public class RubricService : IRubricService
    {
        private readonly IStaticDataService _staticDataService;

        public RubricService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public async Task AssociateRubricAsync(BsdEntity bsd)
        {
            foreach (var employee in bsd.Employees)
            {
                ICollection<Rubric> allowedRubrics = await GetAllowedRubric(employee, bsd.DayType);

                foreach (var rubric in allowedRubrics)
                {
                    employee.Rubrics.Add(rubric);
                }
            }
        }


        public async Task<ICollection<Rubric>> GetAllowedRubric(Employee employee, DayType dayType)
        {
            var rubrics = await _staticDataService.GetRubrics();

            return dayType switch
            {
                DayType.Workday => rubrics
                    .Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Workday)
                    .ToList(),
                DayType.Holiday => rubrics
                    .Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Holiday)
                    .ToList(),
                DayType.Sunday => rubrics
                    .Where(r => r.ServiceType == employee.ServiceType && r.DayType == DayType.Sunday)
                    .ToList(),
                _ => new List<Rubric>(),
            };
        }
    }
}