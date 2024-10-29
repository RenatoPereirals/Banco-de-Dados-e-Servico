using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using System.Net;

using Microsoft.AspNetCore.Mvc;
using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IStaticDataService _staticDataService;

        public ReportController(IReportService reportService,
                                 IStaticDataService staticData)
        {
            _reportService = reportService;
            _staticDataService = staticData;
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GenerateReportById([FromBody] ReportRequest request, int id)
        {
            IEnumerable<Employee> emplyess = _staticDataService.GetEmployees().Where(e => e.EmployeeId == id)
                ?? throw new Exception("Employees not found");

            var result = await _reportService.ProcessReportByIdAsync(request, emplyess);

            if (result)
                return Ok("Report generation initiated successfully.");
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to generate the report.");
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GenerateReport([FromBody] ReportRequest request)
        {
            var result = await _reportService.ProcessReportAsync(request);

            if (result)
                return Ok("Report generation initiated successfully.");
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to generate the report.");
        }
    }
}
