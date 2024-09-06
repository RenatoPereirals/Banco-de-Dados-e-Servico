using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Entities;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Application.Services
{
    public class MarkService : IMarkService
    {
        private readonly IMarkProcessor _mappingMark;
        private readonly IEmployeeService _employeeService;
        private readonly IExternalApiService _externalApiService;

        public MarkService(IMarkProcessor mappingMark,
                           IEmployeeService employeeService,
                           IExternalApiService externalApiService)
        {
            _mappingMark = mappingMark;
            _employeeService = employeeService;
            _externalApiService = externalApiService;
        }

        public async Task<ICollection<MarkResponse>> GetMarksForEmployeesAsync(IEnumerable<Employee> employees, ReportRequest request)
        {
            var employeeIds = employees.Select(e => e.EmployeeId).ToList();
            var markRequest = new MarkRequest(employeeIds)
            {
                DataInicio = request.DataInicio,
                DataFim = request.DataFim
            };

            return await _externalApiService.GetMarkAsync(markRequest);
        }

        public ICollection<Employee> ProcessMarksAsync(ICollection<MarkResponse> markResponses)
        {
            if (markResponses == null || !markResponses.Any())
                throw new ArgumentException("Mark responses cannot be null or empty.", nameof(markResponses));            

            var employees = _mappingMark.ProcessMarks(markResponses);

            return employees ??
                throw new InvalidOperationException("Failed to process BSD.");
        }
    }
}
