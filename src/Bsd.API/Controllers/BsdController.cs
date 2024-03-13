using Bsd.Application.Interfaces;
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
        private readonly IBsdApplicationService _bsdApplication;

        public BsdController(IBsdRepository bsdRepository,
                             IGeralRepository geralRepository,
                             IBsdService bsdService,
                             IBsdApplicationService bsdApplication)
        {
            _bsdRepository = bsdRepository;
            _geralRepository = geralRepository;
            _bsdService = bsdService;
            _bsdApplication = bsdApplication;

        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetEmployeeBsdEntities(string startDate, string endDate)
        {
            try
            {
                var employeeBsdEntities = await _bsdApplication.GetBsdEntitiesDtoByDateRangeAsync(startDate, endDate);

                if (employeeBsdEntities == Empty || employeeBsdEntities == null)
                    return BadRequest($"Nenhum BSD pode ser encontrado entre as datas {startDate} e {endDate}.");

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

        [HttpPost("create")]
        public async Task<IActionResult> CreatedBsdEntity(int bsdNumber, string dateService, int employeeRegistration, int digit)
        {
            try
            {
                await _bsdApplication.CreateBsdAsync(bsdNumber, dateService, employeeRegistration, digit);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployeeToBsdEntity(int bsdNumber)
        {
            try
            {
                var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
