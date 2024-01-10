using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bsd.Domain.Service.Interfaces;

namespace Bsd.Domain.Service
{
    public class PortuaryDayAdjuster : IHolidayAdjuster
    {
        public DateTime Adjust(DateTime date)
        {
            // Verificar se o feriado é o Dia do Portuário (28/1)
            if (date.Month == 1 && date.Day == 28)
            {
                // Verificar se o Dia do Portuário cai em um sábado ou domingo
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    // Se for sábado, ajustar para segunda-feira
                    return date.AddDays(2);
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    // Se for domingo, ajustar para segunda-feira
                    return date.AddDays(1);
                }
            }

            return date;
        }
    }
}