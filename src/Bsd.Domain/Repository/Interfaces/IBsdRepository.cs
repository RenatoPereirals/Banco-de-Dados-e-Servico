namespace Bsd.Domain.Repository.Interfaces
{
    public interface IBsdRepository : ICreatableUpdatable<Bsd.Domain.Entities.Bsd> 
    {
        Task<Entities.Bsd> GetBsdByIdAsync(string bsdId);
    }
}