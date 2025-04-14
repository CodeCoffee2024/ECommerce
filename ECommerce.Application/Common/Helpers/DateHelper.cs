using System.Globalization;

namespace ECommerce.Application.Common.Helpers
{
    public static class DateHelper
    {
        #region Fields

        private const string DateFormat = "yyyy-MM-dd";

        #endregion Fields

        #region Public Methods

        public static string ToFormattedDate(DateTime date)
        {
            return date.ToString(DateFormat);
        }

        public static DateTime ToDateTime(string dateString)
        {
            return DateTime.ParseExact(dateString, DateFormat, CultureInfo.InvariantCulture);
        }

        public static DateTime? ToNullableDateTime(string? dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return null;

            if (DateTime.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                return date;

            return null;
        }

        #endregion Public Methods
    }
}