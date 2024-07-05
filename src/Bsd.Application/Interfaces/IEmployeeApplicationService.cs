using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IEmployeeApplicationService
    {
        Task<EmployeeRequestDto> CreateEmployeeAsync(EmployeeRequestDto request);
    }
}