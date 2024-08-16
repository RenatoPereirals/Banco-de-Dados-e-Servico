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
        private readonly IBsdApplicationService _bsdApplication;

        public ReportController(IBsdApplicationService bsdApplication)
        {
            _bsdApplication = bsdApplication;
        }

        [HttpPost("generate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GenerateReport([FromBody] ReportRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(nameof(ModelState));

            var result = await _bsdApplication.GenerateReportAsync(request);

            if (result)
                return Ok("Report generation initiated successfully.");
            else
                return StatusCode((int)HttpStatusCode.InternalServerError, "Failed to generate the report.");

        }
    }
}
