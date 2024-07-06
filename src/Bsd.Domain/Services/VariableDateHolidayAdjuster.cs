using Bsd.Domain.Service.Interfaces;

namespace Bsd.Domain.Service
{
    public class VariableDateHolidayAdjuster : IVariableDateHolidayAdjuster
    {
        public bool IsVariableHoliday(DateTime date)
        {
            return IsPortuaryDay(date) || IsEaster(date) || IsCarnival(date) || IsGoodFriday(date) || IsCorpusChristi(date);
        }

        private static bool IsPortuaryDay(DateTime date)
        {
            if (date.Month == 1 && (date.Day == 28 || date.Day == 29 || date.Day == 30))
            {
                if (date.Day == 28 && (
                    date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                {
                    return false;
                }
                if ((date.Day == 29 || date.Day == 30) && date.DayOfWeek != DayOfWeek.Monday)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsEaster(DateTime date)
        {
            DateTime easterDate = CalculateEasterDate(date.Year);
            return date.Date == easterDate.Date;
        }

        private static bool IsCarnival(DateTime date)
        {
            DateTime easterDate = CalculateEasterDate(date.Year);
            DateTime carnivalDate = easterDate.AddDays(-47); // Carnaval é 47 dias antes da Páscoa
            return date.Date == carnivalDate.Date;
        }

        private static bool IsGoodFriday(DateTime date)
        {
            DateTime easterDate = CalculateEasterDate(date.Year);
            DateTime goodFridayDate = easterDate.AddDays(-2); // Sexta-feira Santa é dois dias antes da Páscoa
            return date.Date == goodFridayDate.Date;
        }

        private static bool IsCorpusChristi(DateTime date)
        {
            DateTime easterDate = CalculateEasterDate(date.Year);
            DateTime corpusChristiDate = easterDate.AddDays(60); // Corpus Christi é 60 dias após a Páscoa
            return date.Date == corpusChristiDate.Date;
        }

        private static DateTime CalculateEasterDate(int year)
        {
            int goldenNumber = year % 19 + 1;
            int century = year / 100 + 1;
            int skippedLeapYears = 3 * century / 4 - 12;
            int correctionFactor = (8 * century + 5) / 25 - 5;
            int sunday = 5 * year / 4 - skippedLeapYears - 10;
            int epact = (11 * goldenNumber + 20 + correctionFactor - skippedLeapYears) % 30;

            if ((epact == 25 && goldenNumber > 11) || epact == 24)
            {
                epact++;
            }

            int fullMoon = 44 - epact;
            fullMoon += 30 * (fullMoon < 21 ? 1 : 0);
            fullMoon += 7 - ((sunday + fullMoon) % 7);

            int day = fullMoon > 31 ? fullMoon - 31 : fullMoon;
            int month = fullMoon > 31 ? 4 : 3;

            return new DateTime(year, month, day);
        }
    }
}
