using Bsd.Application.DTOs;

namespace Bsd.Application.Interfaces
{
    public interface IDateHelper
    {
        DateTime ParseDate(string date);
        DateTime CreateDateTime(int year, int month, int day, int hour = 0, int minute = 0);
        DateTime CreateDateTimeFromMark(MarkResponse mark);
    }
}