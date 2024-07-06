using Bsd.Domain.Services.Interfaces;

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

            try
            {
                string dateDecoded = System.Net.WebUtility.UrlDecode(date);
                string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy","d/M/yyyy", "yyyy-MM-dd" };
                if (DateTime.TryParseExact(dateDecoded, formats, _culture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    throw new FormatException("Invalid date format");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse date: {ex.Message}");
                throw new FormatException("Failed to parse date");
            }
        }
    }
}
