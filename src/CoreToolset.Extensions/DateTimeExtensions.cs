namespace CoreToolset.Extensions
{
    public static class DateTimeExtensions
    {
        #region Is, IsNot
        public static bool IsPassed(this DateTime dateTime, int years = 0, int months = 0, int days = 0, int hours = 0, int minutes = 0, int seconds = 0)
        {
            ArgumentNullException.ThrowIfNull(dateTime);

            DateTime expiryDateTime = dateTime
                .AddYears(years)
                .AddMonths(months)
                .AddDays(days)
                .AddHours(hours)
                .AddMinutes(minutes)
                .AddSeconds(seconds);

            return DateTime.Now > expiryDateTime;
        }
        public static bool IsNotPassed(this DateTime @this, int years = 0, int months = 0, int days = 0, int hours = 0, int minutes = 0, int seconds = 0)
        {
            ArgumentNullException.ThrowIfNull(@this);

            return !@this.IsPassed(years, months, days, hours, minutes, seconds);
        }
        public static bool IsWithinTimeRange(this DateTime @this, TimeSpan startTime, TimeSpan endTime)
            => @this.TimeOfDay >= startTime && @this.TimeOfDay <= endTime;

        public static bool IsDateTimeBetween(this DateTime @this, DateTime rangeStart, DateTime rangeEnd)
            => (@this >= rangeStart && @this <= rangeEnd) || (@this <= rangeStart && @this >= rangeEnd);
        public static bool IsFirstDayOfMonth(this DateTime @this)
            => @this.Day == 1;
        public static bool IsLastDayOfMonth(this DateTime @this)
            => @this.Day == DateTime.DaysInMonth(@this.Year, @this.Month);
        public static bool IsFirstDayOfWeek(this DateTime @this)
            => @this.DayOfWeek == DayOfWeek.Monday;
        public static bool IsLastDayOfWeek(this DateTime @this)
            => @this.DayOfWeek == DayOfWeek.Sunday;
        public static bool IsWeekend(this DateTime @this)
            => @this.DayOfWeek == DayOfWeek.Saturday || @this.DayOfWeek == DayOfWeek.Sunday;
        public static bool IsWeekday(this DateTime @this)
            => !@this.IsWeekend();
        public static bool IsEvenYear(this DateTime @this)
            => @this.Year % 2 == 0;
        public static bool IsOddYear(this DateTime @this)
            => !@this.IsEvenYear();
        public static bool IsEvenMonth(this DateTime @this)
            => @this.Month % 2 == 0;
        public static bool IsOddMonth(this DateTime @this)
            => !@this.IsEvenMonth();
        #endregion

        #region Until
        public static int DaysUntil(this DateTime @this, DateTime until)
            => (int)(until - @this).TotalDays;

        public static int WeeksUntil(this DateTime @this, DateTime until)
            => (int)(until - @this).TotalDays / 7;

        public static int MonthsUntil(this DateTime @this, DateTime until)
            => (until.Year - @this.Year) * 12 + until.Month - @this.Month;

        public static int YearsUntil(this DateTime @this, DateTime until)
            => until.Year - @this.Year;
        #endregion

        #region Add
        public static DateTime AddWeeks(this DateTime @this, int weeks)
            => @this.AddDays(7 * weeks);

        public static DateTime AddBusinessDays(this DateTime @this, int days)
        {
            int sign = Math.Sign(days);
            while (days != 0)
            {
                @this = @this.AddDays(sign);
                if (@this.DayOfWeek != DayOfWeek.Saturday && @this.DayOfWeek != DayOfWeek.Sunday)
                    days -= sign;
            }
            return @this;
        }

        public static DateTime AddBusinessDays(this DateTime @this, int days, params DateTime[] holidays)
        {
            int sign = Math.Sign(days);
            while (days != 0)
            {
                @this = @this.AddDays(sign);
                if (@this.DayOfWeek != DayOfWeek.Saturday && @this.DayOfWeek != DayOfWeek.Sunday && !holidays.Contains(@this))
                    days -= sign;
            }
            return @this;
        }
        #endregion

        #region To
        public static long ToUnixTimestamp(this DateTime dateTime)
            => (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        #endregion

    }
}
    