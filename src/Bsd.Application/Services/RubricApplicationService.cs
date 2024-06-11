using Bsd.Application.Interfaces;
using Bsd.Domain.Enums;
using Bsd.Domain.Repository.Interfaces;

namespace Bsd.Application.Services
{
    public class RubricApplicationService : IRubricApplicationService
    {
        private readonly IRubricRepository _rubricRepository;

        public RubricApplicationService(IRubricRepository rubricRepository)
        {
            _rubricRepository = rubricRepository;
        }

        public async Task CreateRubricAsync(int rubricId, string description, decimal hoursPerDay, string dayType, string serviceType)
        {
            try
            {
                dayType = dayType.Replace(" ", "");
                serviceType = serviceType.Replace(" ", "");

                if (!Enum.TryParse(dayType, true, out DayType day))
                {
                    throw new ArgumentException($"O tipo de dia {dayType} não existe.", nameof(dayType));
                }

                if (!Enum.TryParse(serviceType, true, out ServiceType service))
                {
                    throw new ArgumentException($"O tipo de serviço {serviceType} não existe.", nameof(serviceType));
                }

                await _rubricRepository.CreateRubricAsync(rubricId, description, hoursPerDay, day, service);
            }
            catch (ArgumentException ex)
            {
                throw new Exception($"Erro ao tentar criar rubrica. Error: {ex.Message}");
            }
        }
    }
}