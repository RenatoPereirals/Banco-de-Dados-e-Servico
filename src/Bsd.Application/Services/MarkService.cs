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

        public MarkService(IMarkProcessor mappingMark, IEmployeeService employeeService)
        {
            _mappingMark = mappingMark;
            _employeeService = employeeService;
        }

        public async Task<BsdEntity> ProcessMarksAsync(ICollection<MarkResponse> markResponses)
        {
            if (markResponses == null || !markResponses.Any())
            {
                throw new ArgumentException("Mark responses cannot be null or empty.", nameof(markResponses));
            }

            var employees = _mappingMark.ProcessMarks(markResponses); // aqui a data de inicio e fim dos dias trabalhados e as horas de entrada e saída estão sendo associadas corretamente

            var employeeIds = markResponses.Select(mark => mark.Matricula).ToList();
            var bsdEntity = await _employeeService.AssociateEmployeesToBsdAsync(employees)
                ?? throw new InvalidOperationException("Failed to process BSD.");

            return bsdEntity ??
                throw new InvalidOperationException("Failed to process BSD.");
        }
    }
}
