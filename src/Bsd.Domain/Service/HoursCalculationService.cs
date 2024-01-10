using Bsd.Domain.Entities;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;
using Bsd.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsd.Domain.Services
{
    public class OvertimeCalculationService : IHoursCalculationService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBsdRepository _bsdRepository;
        private readonly IRubricRepository _rubricRepository;

        public OvertimeCalculationService(IEmployeeRepository employeeRepository,
                                          IBsdRepository bsdRepository,
                                          IRubricRepository rubricRepository)
        {
            _employeeRepository = employeeRepository;
            _bsdRepository = bsdRepository;
            _rubricRepository = rubricRepository;
        }

        public async Task<decimal> CalculateOvertimeHours(string registration, DateTime dateService, DayType dayType)
        {
            var employee = await _employeeRepository.GetEmployeeByRegistrationAsync(registration) ?? throw new Exception("Funcionário não encontrado");

            var rubrics = CalculateOvertimeBasedOnDayType(dayType, employee.ServiceType);

            // Faça algo com as rubricas, como somar as horas extras de cada rubrica
            decimal totalOvertimeHours = rubrics.Sum(r => r.HoursPerDay);

            return totalOvertimeHours;
        }

        public Task<decimal> CalculateOvertimeHours(string registration, DateTime dateService, DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        private List<Rubric> CalculateOvertimeBasedOnDayType(DayType dayType, ServiceType serviceType)
        {
            // Recupera todas as rubricas aplicáveis para o tipo de dia e tipo de serviço
            var rubrics = _rubricRepository.GetAll()
                .Where(r => r.DayType == dayType && (r.ServiceType == serviceType))
                .ToList();

            return rubrics;
        }

        private void CalculateOvertimeBaseWorkday(ServiceType serviceType)
        {
            var rubricList = new List<Rubric>();
            if (serviceType == ServiceType.P140)
            {
                rubricList.Add(new Rubric("asdf", "Descrição da Rubrica", 3.0M, DayType.Workday, ServiceType.P140));
            }
            else if (serviceType == ServiceType.P110)
            {
                rubricList.Add(new Rubric("asdf", "Descrição da Rubrica", 3.0M, DayType.Workday, ServiceType.P110));
            }
            else
            {
                throw new Exception("Tipo de serviço não encontrado!");
            }
        }
    }
}
