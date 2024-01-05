using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bsd.Domain.Service.Interfaces
{
    public interface IHolidayAdjuster
    {
        DateTime Adjust(DateTime date);
    }
}