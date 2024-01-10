namespace Bsd.Domain.Repository.Interfaces
{
    public interface ICreatableUpdatable<T>
    {
        void Create(T entity);
        void Update(T entity);
    }
}