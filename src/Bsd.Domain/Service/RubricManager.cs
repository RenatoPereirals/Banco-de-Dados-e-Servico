using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

public class RubricManager
{
    private List<Rubric> rubrics = new List<Rubric>();

    // Construtor que recebe as rubricas iniciais
    public RubricManager(IEnumerable<Rubric> initialRubrics = null)
    {
        if (initialRubrics != null)
        {
            rubrics.AddRange(initialRubrics);
        }
    }

    public void AddRubric(string code, string description, decimal hoursPerDay, DayType dayType, ServiceType serviceType)
    {
        Rubric newRubric = new Rubric(code, description, hoursPerDay, dayType, serviceType);
        rubrics.Add(newRubric);
    }

    public List<Rubric> GetRubrics()
    {
        return rubrics;
    }
}
