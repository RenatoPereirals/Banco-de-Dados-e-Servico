using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Domain.Service
{    
    public class RubricManager
    {
        private readonly List<Rubric> rubrics = new();

        // Construtor que recebe as rubricas iniciais
        public RubricManager(IEnumerable<Rubric> initialRubrics)
        {
            if (initialRubrics != null)
            {
                rubrics.AddRange(initialRubrics);
            }
        }

        public void AddRubric(string code, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType)
        {
            Rubric newRubric = new(code, description, hoursPerDay, dayType, serviceType);
            rubrics.Add(newRubric);
        }

        public List<Rubric> GetRubrics()
        {
            return rubrics;
        }
    }
}
