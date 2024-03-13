namespace Bsd.Application.Interfaces
{
    public interface IRubricApplicationService
    {
        Task CreateRubricAsync(string code, string description, decimal hoursPerDay, string dayType, string serviceType);
    }
}