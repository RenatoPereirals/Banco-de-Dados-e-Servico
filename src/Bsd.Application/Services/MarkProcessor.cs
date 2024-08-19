using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Application.Services;
public class MarkProcessor : IMarkProcessor
{
    public ICollection<BsdEntity> ProcessMarks(ICollection<MarkResponse> marks)
    {
        var groupedMarks = marks
            .GroupBy(m => m.Matricula)
            .Select(g => g.OrderBy(m => new DateTime(m.Ano, m.Mes, m.Dia, m.Hora, m.Minuto, 0)).ToList())
            .ToList();

        var bsdEntities = new List<BsdEntity>();

        foreach (var employeeMarks in groupedMarks)
        {
            var startMark = employeeMarks.First();
            var endMark = startMark;

            for (int i = 1; i < employeeMarks.Count; i++)
            {
                var currentMark = employeeMarks[i];
                var currentDate = new DateTime(currentMark.Ano, currentMark.Mes, currentMark.Dia);
                var endDate = new DateTime(endMark.Ano, endMark.Mes, endMark.Dia);

                if ((currentDate - endDate).Days == 1)
                {
                    endMark = currentMark;
                }
                else
                {
                    bsdEntities.Add(CreateBsdEntity(startMark, endMark));
                    startMark = currentMark;
                    endMark = currentMark;
                }
            }

            bsdEntities.Add(CreateBsdEntity(startMark, endMark));
        }

        return bsdEntities;
    }

    private BsdEntity CreateBsdEntity(MarkResponse startMark, MarkResponse endMark)
    {
        return new BsdEntity
        {
            DateService = new DateTime(startMark.Ano, startMark.Mes, startMark.Dia),
            StartTime = new TimeOnly(startMark.Hora, startMark.Minuto),
            EndTime = new TimeOnly(endMark.Hora, endMark.Minuto),
        };
    }
}
