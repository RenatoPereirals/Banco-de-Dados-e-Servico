
using AutoMapper;

using Bsd.Application.Interfaces;
using Bsd.Application.DTOs;

using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;

namespace Bsd.Application.Services
{
    public class EmployeeApplicationService : IEmployeeApplicationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeApplicationService(IEmployeeService employeeService,
                                          IEmployeeRepository employeeRepository,
                                          IMapper mapper)
        {
            _employeeService = employeeService;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeRequestDto> CreateEmployeeAsync(EmployeeRequestDto request)
        {
            RequestValidation(request);

            var employee = _mapper.Map<Employee>(request);

            var serviceType = request.ServiceType.Replace(" ", "");

            if (!Enum.TryParse(serviceType, out ServiceType service))
            {
                throw new ArgumentException($"O tipo de serviço {serviceType} não existe.", nameof(serviceType));
            }

            var digit = _employeeService.CalculateModulo11CheckDigit(request.EmployeeId);

            employee.Digit = digit;

            if (await _employeeRepository.CreateEmployeeAsync(employee))
            {
                var employeeReturn = await _employeeRepository.GetEmployeeByRegistrationAsync(employee.EmployeeId);
                return _mapper.Map<EmployeeRequestDto>(employeeReturn);
            }

            throw new ApplicationException("Falha ao criar funcionário no banco de dados.");
        }

        private static void RequestValidation(EmployeeRequestDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request), "Você precisa preencher todos os dados.");
            }

            var bsdNumer = request.EmployeeId.ToString();

            if (bsdNumer.Length != 4)
            {
                throw new ArgumentOutOfRangeException("O número da matrícula deve conter 4 digitos.", nameof(request));
            }

            if (request.EmployeeId <= 0)
            {
                throw new ArgumentOutOfRangeException("O número da matrícula não pode ser negativo.", nameof(request));
            }

            if (string.IsNullOrWhiteSpace(request.ServiceType))
            {
                throw new ArgumentException("O serviço não pode ser vazio ou nulo.", nameof(request));
            }
        }
    }
}