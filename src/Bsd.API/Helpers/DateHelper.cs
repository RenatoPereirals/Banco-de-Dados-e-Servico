using System.Globalization;

namespace Bsd.API.Helpers
{
    public static class DateHelper
    {
        public static DateTime ParseDate(string date)
        {
            string dateDecoded = System.Net.WebUtility.UrlDecode(date);
            return  DateTime.ParseExact(dateDecoded, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        } 
    }
}