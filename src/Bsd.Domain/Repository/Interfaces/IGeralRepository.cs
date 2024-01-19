namespace Bsd.Domain.Repository.Interfaces
{
    public interface IGeralRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
    }
}