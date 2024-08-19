using Bsd.Application.Interfaces;

using Bsd.Application.DTOs;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Application.Services
{
    public class MarkService : IMarkService
    {
        private readonly IBsdService _bsdService;
        private readonly IEmployeeService _employeeService;
        private readonly IMarkProcessor _mappingMark;

        public MarkService(IBsdService bsdService,
                           IEmployeeService employeeService,
                           IMarkProcessor mappingMark)
        {
            _bsdService = bsdService;
            _employeeService = employeeService;
            _mappingMark = mappingMark;
        }

        public async Task<ICollection<BsdEntity>> ProcessMarksAsync(ICollection<MarkResponse> markResponses)
        {
            if (markResponses == null || !markResponses.Any())
                throw new ArgumentException("Mark responses cannot be null or empty.", nameof(markResponses));

            var bsdEntities = _mappingMark.ProcessMarks(markResponses);

            var employeeIds = markResponses.Select(mark => mark.Matricula).ToList();

            await _employeeService.AssociateEmployeesToBsdAsync(bsdEntities, employeeIds);

            var processedBsdEntities = await _bsdService.CreateOrUpdateBsdsAsync(bsdEntities);

            return processedBsdEntities ??
                throw new InvalidOperationException("Failed to process BSD.");
        }
    }
}
