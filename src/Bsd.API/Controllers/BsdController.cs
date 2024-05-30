using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bsd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BsdController : ControllerBase
    {
        private readonly IBsdRepository _bsdRepository;
        private readonly IBsdApplicationService _bsdApplication;
        private readonly IEmployeeRepository _employeeRepository;


        public BsdController(IBsdRepository bsdRepository,
                             IBsdApplicationService bsdApplication,
                             IEmployeeRepository employeeRepository)
        {
            _bsdRepository = bsdRepository;
            _bsdApplication = bsdApplication;
            _employeeRepository = employeeRepository;

        }

        [HttpGet("{startDate}/{endDate}")]
        public async Task<IActionResult> GetBsdByDate(string startDate, string endDate)
        {
            try
            {
                var bsd = await _bsdApplication.GetBsdEntitiesDtoByDateRangeAsync(startDate, endDate);
                if (bsd == Empty || bsd == null)
                    return BadRequest($"Nenhum BSD pode ser encontrado entre as datas {startDate} e {endDate}.");


                return Ok(bsd);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> GetBsdById(int bsdNumber)
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
        public async Task<IActionResult> Post([FromBody] CreateBsdRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(request.EmployeeRegistration);

                if (employee == null)
                    return BadRequest($"Funcionário com a matrícula {request.EmployeeRegistration} não encontrado.");

                var createdBsd = await _bsdApplication.CreateBsdAsync(request);

                if (createdBsd == null)
                    return NoContent();

                return CreatedAtAction(nameof(GetBsdById), new { id = createdBsd.BsdNumber }, createdBsd);
            }
            catch (ApplicationException)
            {
                // _logger.LogError(ex, "Erro ao tentar criar BSD.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro interno. Por favor, tente novamente.");
            }
            catch (Exception)
            {
                // _logger.LogError(ex, "Erro inesperado ao tentar criar BSD.");
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  "Ocorreu um erro inesperado. Por favor, tente novamente.");
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
