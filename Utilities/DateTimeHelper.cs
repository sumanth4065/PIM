using PIMQAAutomation.Modules.Common.Constants;

namespace PIMQAAutomation.Modules.Common.Helpers
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Converts a UTC datetime (from DB) to local timezone (used by UI).
        /// </summary>
        /// <param name="dbDateUtc">The UTC datetime from the database.</param>
        /// <returns>Datetime converted to local timezone.</returns>
        public static string ConvertDbUtcToLocal(string dbDateUtc, string? timeZoneId = null)
        {
            if (!DateTime.TryParse(dbDateUtc, out var date))
            {
                Console.WriteLine($"Invalid DB Date format: {dbDateUtc}");
                return string.Empty;
            }
            // Ensure DB datetime is UTC
            DateTime dbUtcDate = DateTime.SpecifyKind(date, DateTimeKind.Utc);

            // Get frontend timezone
            TimeZoneInfo frontEndZone = TimeZoneInfo.Local;
            Console.WriteLine($"Frontend TimeZone: {frontEndZone.DisplayName}");

            // Convert from UTC → Local
            DateTime dbLocalDate = TimeZoneInfo.ConvertTimeFromUtc(dbUtcDate, frontEndZone);
            Console.WriteLine($"Converted DB Local Date: {dbLocalDate:yyyy-MM-dd HH:mm:ss}");

            // Return formatted string (only date)
            return dbLocalDate.ToString(TimeConstants.DateFormat);
        }
    }
}
