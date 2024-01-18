using Bsd.Domain.Enums;

namespace Bsd.Domain.Entities
{
    public class RubricConfiguration
    {
        private readonly List<Rubric> rubrics = new();

        public RubricConfiguration(IEnumerable<Rubric> initialRubrics)
        {
            if (initialRubrics != null)
            {
                rubrics.AddRange(initialRubrics);
            }
        }

        public ServiceType ServiceType { get; set; }
        public DayType DayType { get; set; }
        public List<Rubric> Rubrics { get; set; } = new();
    }
}