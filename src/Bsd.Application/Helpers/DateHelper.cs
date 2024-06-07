using System.Globalization;

namespace Bsd.API.Helpers
{
    public static class DateHelper
    {
        public static DateTime ParseDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                throw new ArgumentNullException(nameof(date), "o campo data é obrigatória");
            }

            try
            {
                string dateDecoded = System.Net.WebUtility.UrlDecode(date);
                string[] formats = { "dd/MM/yyyy" };
                if (DateTime.TryParseExact(dateDecoded, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }
                else
                {
                    // _logger.LogError(ex, "Invalid date format");
                    throw new FormatException("Invalid date format");
                }
            }
            catch (Exception)
            {
                // _logger.LogError(ex, "Failed to parse date");
                throw new FormatException("Failed to parse date");
            }
        }
    }
}