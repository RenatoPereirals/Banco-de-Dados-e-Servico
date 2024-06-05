
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;

namespace Bsd.Application.Services
{
    public class EmployeeApplicationService : IEmployeeApplicationService
    {
        private readonly IEmployeeService _employeeService;
        private readonly IGeralRepository _geralRepository;

        public EmployeeApplicationService(IGeralRepository geralRepository,
                                          IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _geralRepository = geralRepository;
        }

        public async Task CreateEmployeeAsync(int registration, string serviceType)
        {
            try
            {
                serviceType = serviceType.Replace(" ", "");

                if (!Enum.TryParse(serviceType, out ServiceType service))
                {
                    throw new ArgumentException($"O tipo de serviço {serviceType} não existe.", nameof(serviceType));
                }

                var digit = _employeeService.CalculateModulo11CheckDigit(registration);

                var employee = new Employee();
                _geralRepository.Create(employee);

                await _geralRepository.SaveChangesAsync();

            }
            catch (ArgumentException ex)
            {

                throw new Exception($"Erro ao tentar cria funcionário. Error: {ex.Message}");
            }
        }
    }
}