using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;
using Bsd.Domain.Entities;

namespace Bsd.Application.Services
{
    public class MarkProcessor : IMarkProcessor
    {
        public ICollection<Employee> ProcessMarks(ICollection<MarkResponse> marks)
        {
            // Agrupa as marcações por funcionário
            var groupedMarks = marks
                .GroupBy(m => m.Matricula)
                .Select(g => g.OrderBy(m => new DateTime(m.Ano, m.Mes, m.Dia, m.Hora, m.Minuto, 0)).ToList())
                .ToList();

            var employees = new List<Employee>();

            foreach (var employeeMarks in groupedMarks)
            {
                var employee = new Employee
                {
                    EmployeeId = employeeMarks.First().Matricula,
                    WorkedDays = new List<WorkedDay>()
                };

                var startMark = employeeMarks.OrderBy(m => new DateTime(m.Ano, m.Mes, m.Dia, m.Hora, m.Minuto, 0)).First();
                var endMark = startMark;

                for (int i = 1; i < employeeMarks.Count; i++)
                {
                    var currentMark = employeeMarks[i];
                    var currentDate = new DateTime(currentMark.Ano, currentMark.Mes, currentMark.Dia);
                    var endDate = new DateTime(endMark.Ano, endMark.Mes, endMark.Dia);

                    // Se as datas forem consecutivas, atualiza o endMark
                    if (currentDate.Date == endDate.Date.AddDays(1))
                    {
                        endMark = currentMark;
                    }
                    else
                    {
                        // Adiciona um WorkedDay ao Employee
                        employee.WorkedDays.Add(CreateWorkedDay(startMark, endMark));

                        // Reinicia o startMark e endMark para o próximo grupo de dias consecutivos
                        startMark = currentMark;
                        endMark = currentMark;
                    }
                }

                // Adiciona o último WorkedDay do funcionário
                employee.WorkedDays.Add(CreateWorkedDay(startMark, endMark));

                // Adiciona o Employee à lista de funcionários
                employees.Add(employee);
            }

            return employees;
        }

        private WorkedDay CreateWorkedDay(MarkResponse startMark, MarkResponse endMark)
        {
            return new WorkedDay
            {
                DateEntry = new DateTime(startMark.Ano, startMark.Mes, startMark.Dia),
                DateExit = new DateTime(endMark.Ano, endMark.Mes, endMark.Dia),
                StartTime = new TimeOnly(startMark.Hora, startMark.Minuto),
                EndTime = new TimeOnly(endMark.Hora, endMark.Minuto)
            };
        }
    }
}
