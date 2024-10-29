using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;


namespace Bsd.Application.Services
{
    public class BsdApplicationService : IBsdApplicationService
    {
        private readonly IExternalApiService _externalApiService;
        private readonly IBsdService _bsdService;

        public BsdApplicationService(IExternalApiService externalApiService,
                                     IBsdService bsdService)
        {
            _externalApiService = externalApiService;
            _bsdService = bsdService;
        }

        public async Task<ICollection<MarkResponse>> GetMarksForEmployeesAsync(ICollection<Employee> employees, ReportRequest request)
        {
            var employeeIds = employees.Select(e => e.EmployeeId).ToList();
            var markRequest = new MarkRequest(employeeIds)
            {
                DataInicio = request.DataInicio,
                DataFim = request.DataFim
            };
            return await _externalApiService.GetMarkAsync(markRequest);
        }

        // Should create a new `BsdEntity` with the processed employees
        public async Task<BsdEntity> CreatebsdEntityAsync(ICollection<Employee> employees)
        {            
            var bsdEntity = new BsdEntity
            {
                Employees = employees
            };

            await _bsdService.CreateOrUpdateBsdsAsync(bsdEntity);

            return bsdEntity;
        }
    }
}