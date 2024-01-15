using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;

namespace Bsd.Domain.Repository
{
    public class RubricRepository
    {
        private readonly List<Rubric> _rubrics;

        public RubricRepository()
        {
            // Inicializa a lista com rubricas padrão
            _rubrics = new List<Rubric>
            {
                new("1902", "3h/dia de 50% (hora extra)", 3.0M, DayType.Workday, ServiceType.P140),
                new("1913", "2h/dia de 100% (hora de refeição)", 2.0M, DayType.Workday, ServiceType.P140),
                new("1921", "3h/dia de 150% (hora extra)", 3.0M, DayType.Workday, ServiceType.P140),
            };
        }
        public void Add(Rubric rubric)
        {
            _rubrics.Add(rubric);
        }

        public IEnumerable<Rubric> GetAll()
        {
            return _rubrics;
        }
    }
}