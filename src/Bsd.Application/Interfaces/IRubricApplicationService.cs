namespace Bsd.Application.Interfaces
{
    public interface IRubricApplicationService
    {
        Task CreateRubricAsync(int rubricId, string description, decimal hoursPerDay, string dayType, string serviceType);
    }
}