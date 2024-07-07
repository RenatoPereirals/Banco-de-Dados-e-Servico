namespace Bsd.Domain.Repository.Interfaces
{
    public interface IGeralRepository
    {
        void Create<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteRange<T>(T[] entities) where T : class;
        Task<bool> SaveChangesAsync();
    }
}