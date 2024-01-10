using Bsd.Domain.Entities;

namespace Bsd.Domain.Repository.Interfaces
{
    public interface IRubricRepository
    {
        IEnumerable<Rubric> GetAll();
        void Add(Rubric rubric);
    }
}