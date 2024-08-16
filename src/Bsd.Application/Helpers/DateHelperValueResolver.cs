using Bsd.Application.DTOs;

using Bsd.Domain.Entities;

using AutoMapper;

namespace Bsd.Application.Helpers
{
    public class DateHelperValueResolver : IValueResolver<MarkResponse, BsdEntity, DateTime>
    {
        public DateTime Resolve(MarkResponse mark, BsdEntity destination, DateTime destMember, ResolutionContext context)
        {
            return new DateTime(mark.Ano, mark.Mes, mark.Dia, mark.Hora, mark.Minuto, 0);
        }

        
    }

}