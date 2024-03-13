namespace Bsd.Application.Interfaces
{
    public interface IEmployeeApplicationService
    {
        Task CreateEmployeeAsync(int registration, string serviceType);
    }
}