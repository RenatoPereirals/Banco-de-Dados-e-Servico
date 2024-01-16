using Bsd.Domain.Entities;

namespace Bsd.Domain.Services.Interfaces
{
    public interface IDayTypeAndServiceTypeRubricCalculator
    {
        Task<List<Rubric>> CombineRubricsList(string employeeId, string bsdId);
    }
}