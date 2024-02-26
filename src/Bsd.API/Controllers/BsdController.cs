using System.Globalization;
using Bsd.API.Helpers;
using Bsd.Domain.Entities;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsdController : ControllerBase
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IGeralRepository _geralRepository;
        private readonly IBsdService _bsdService;

        public BsdController(IBsdRepository bsdRepository,
                             IGeralRepository geralRepository,
                             IBsdService bsdService)
        {
            _bsdRepository = bsdRepository;
            _geralRepository = geralRepository;
            _bsdService = bsdService;
        }

        [HttpGet("{startDate}-{endDate}")]
        public async Task<IActionResult> GetEmployeeBsdEntities(string startDate, string endDate)
        {
            try
            {
                var parseStartDate = DateHelper.ParseDate(startDate);
                var parseEndDate = DateHelper.ParseDate(endDate);

                var employeeBsdEntities = await _bsdRepository.GetEmployeeBsdEntitiesByDateRangeAsync(parseStartDate, parseEndDate);

                return Ok(employeeBsdEntities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBsdEntiteis()
        {
            try
            {
                var bsdEntities = await _bsdRepository.GetAllBsdAsync();
                return Ok(bsdEntities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{bsdNumber}")]
        public async Task<IActionResult> GetBsdEntity(int bsdNumber)
        {
            try
            {
                var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
                return Ok(bsdEntity);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("created/{bsdNumber}")]
        public async Task<IActionResult> CreatedBsdEntity(int bsdNumber, string dateService, int employeeRegistration, int digit)
        {
            try
            {
                var parseDateService = DateHelper.ParseDate(dateService);
                var bsdEntity = await _bsdService.CreateBsdAsync(bsdNumber, parseDateService, employeeRegistration, digit);

                await _geralRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{bsdNumber}/{employeeRegistration}")]
        public async Task<IActionResult> AddEmployeeToBsdEntity(int bsdNumber, int employeeRegistration, int digit)
        {
            try
            {
                var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
                await _bsdService.AddEmployeesToBsdAsync(bsdEntity, employeeRegistration, digit);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
