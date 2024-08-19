using Bsd.Domain.Services.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Domain.Services
{
    public class BsdService : IBsdService
    {
        private readonly IDayTypeChecker _dayTypeChecker;
        private readonly IRubricService _rubricService;
        private readonly ICalculateRubricHours _calculateRubricHours;

        public BsdService(IDayTypeChecker dayTypeChecker,
                          IRubricService rubricService,
                          ICalculateRubricHours calculateRubricsHours)
        {
            _dayTypeChecker = dayTypeChecker;
            _rubricService = rubricService;
            _calculateRubricHours = calculateRubricsHours;
        }

        public async Task<BsdEntity> CreateBsdAsync(BsdEntity bsd)
        {
            var day = await _dayTypeChecker.GetDayType(bsd.DateService); // verificar se esse método é redundante

            bsd.DayType = day;
            await _rubricService.AssociateRubricAsync(bsd);

            return bsd;
        }

        public async Task<ICollection<BsdEntity>> CreateOrUpdateBsdsAsync(ICollection<BsdEntity> bsds)
        {
            var responses = new List<BsdEntity>();

            _calculateRubricHours.CalculateTotalWorkedHours(bsds);

            foreach (var bsd in bsds)
            {
                await CreateBsdAsync(bsd);
                responses.Add(bsd);
            }

            return responses;
        }
    }
}