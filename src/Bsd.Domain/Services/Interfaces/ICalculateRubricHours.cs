using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces;

public interface ICalculateRubricHours
{
    void CalculateTotalWorkedHours(BsdEntity bsdEntity);
}