using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
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
