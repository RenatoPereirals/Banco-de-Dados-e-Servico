using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsd.Domain.Service.Interfaces
{
    public interface IHoliDayChecker
    {
        bool IsHoliday(DateTime dateTime);
    }
}