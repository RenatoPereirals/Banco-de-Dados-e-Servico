using Bsd.Application.DTOs;
using Bsd.Application.Interfaces;

using System.Globalization;

namespace Bsd.Application.Helpers
{
    public class DateHelper : IDateHelper
    {
        private readonly CultureInfo _culture;

        public DateHelper()
        {
            _culture = new CultureInfo("pt-BR");
        }

        public DateTime ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                Console.WriteLine("The date field is required.");
                throw new ArgumentNullException(nameof(date), "The date field is required.");
            }

            string dateDecoded = System.Net.WebUtility.UrlDecode(date);
            string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy", "d/M/yyyy", "yyyy-MM-dd" };
            if (DateTime.TryParseExact(dateDecoded, formats, _culture, DateTimeStyles.None, out DateTime parsedDate))
                return parsedDate;
            else
                throw new FormatException("Invalid date format");
        }

        public string ParseString(DateTime date)
        {
            return date.ToString("dd/MM/yyyy", _culture);
        }

        public DateTime CreateDateTime(int year, int month, int day, int hour = 0, int minute = 0)
        {
            if (year < 1 || month < 1 || month > 12 || day < 1 || day > DateTime.DaysInMonth(year, month))
                throw new ArgumentException("Invalid date components.");

            if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                throw new ArgumentException("Invalid time components.");

            return new DateTime(year, month, day, hour, minute, 0);
        }

        public DateTime CreateDateTimeFromMark(MarkResponse mark)
        {
            return CreateDateTime(mark.Ano, mark.Mes, mark.Dia, mark.Hora, mark.Minuto);
        }
    }
}

