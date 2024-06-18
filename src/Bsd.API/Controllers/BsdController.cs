using Bsd.Application.DTOs;
using Bsd.Application.Helpers.Interfaces;
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
        private readonly IEmployeeValidationService _employeeValidationService;


        public BsdController(IBsdRepository bsdRepository,
                             IBsdApplicationService bsdApplication,
                             IEmployeeRepository employeeRepository,
                             IEmployeeValidationService employeeValidationService)
        {
            _bsdRepository = bsdRepository;
            _bsdApplication = bsdApplication;
            _employeeRepository = employeeRepository;
            _employeeValidationService = employeeValidationService;

        }

        // [HttpGet("{startDate}/{endDate}")]
        // public async Task<IActionResult> GetBsdByDate(string startDate, string endDate)
        // {
        //     try
        //     {
        //         var bsd = await _bsdApplication.GetBsdEntitiesDtoByDateRangeAsync(startDate, endDate);
        //         if (bsd == Empty || bsd == null)
        //             return BadRequest($"Nenhum BSD pode ser encontrado entre as datas {startDate} e {endDate}.");


        //         return Ok(bsd);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(ex.Message);
        //     }
        // }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bsdEntities = await _bsdRepository.GetAllBsdAsync();
            //return Ok(bsdEntities);
            throw new ArgumentException();
        }

        [HttpGet("{bsdNumber}")]
        public async Task<IActionResult> GetBsdById(int bsdNumber)
        {
            var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
            return Ok(bsdEntity);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] CreateBsdRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var invalidRegistrations = await _employeeValidationService.ValidateEmployeeRegistrationsAsync(request.EmployeeRegistrations);

            if (!invalidRegistrations)
            {
                return BadRequest($"Funcionários com as matrículas {string.Join(", ", request.EmployeeRegistrations)} não encontrados.");
            }

            try
            {
                var createdBsd = await _bsdApplication.CreateBsdAsync(request);

                if (createdBsd == null)
                    return NoContent();

                return CreatedAtAction(nameof(GetBsdById), new { id = createdBsd.BsdNumber }, createdBsd);
            }
            catch (Exception ex) 
            {
                throw new Exception("Erro interno de servidor", ex);
            }

 
        }

        [HttpPost("addEmployee")]
        public async Task<IActionResult> AddEmployeeToBsdEntity(int bsdNumber)
        {
            var bsdEntity = await _bsdRepository.GetBsdByIdAsync(bsdNumber);
            return Ok();
        }
    }
}
