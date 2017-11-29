namespace Apex.System
{
    using SysDateTime = global::System.DateTime;

    public class Date
    {
        internal SysDateTime date;

        internal Date(SysDateTime dt) => date = dt;

        internal Date(int year, int month, int day)
        {
            date = new SysDateTime(year, month, day);
        }

        public Date AddDays(int days)
        {
            ////throw new global::System.NotImplementedException("Date.AddDays");
            return new Date(date.AddDays(days));
        }

        public void AddError(object msg)
        {
            throw new global::System.NotImplementedException("Date.AddError");
        }

        public void AddError(object msg, bool escape)
        {
            throw new global::System.NotImplementedException("Date.AddError");
        }

        public void AddError(string msg)
        {
            throw new global::System.NotImplementedException("Date.AddError");
        }

        public void AddError(string msg, bool escape)
        {
            throw new global::System.NotImplementedException("Date.AddError");
        }

        public Date AddMonths(int months)
        {
            ////throw new global::System.NotImplementedException("Date.AddMonths");
            return new Date(date.AddMonths(months));
        }

        public Date AddYears(int years)
        {
            ////throw new global::System.NotImplementedException("Date.AddYears");
            return new Date(date.AddYears(years));
        }

        public int Day()
        {
            ////throw new global::System.NotImplementedException("Date.Day");
            return date.Day;
        }

        public int DayOfYear()
        {
            ////throw new global::System.NotImplementedException("Date.DayOfYear");
            return date.DayOfYear;
        }

        public int DaysBetween(Date other)
        {
            throw new global::System.NotImplementedException("Date.DaysBetween");
        }

        public static int DaysInMonth(int year, int month)
        {
            throw new global::System.NotImplementedException("Date.DaysInMonth");
        }

        public string Format()
        {
            throw new global::System.NotImplementedException("Date.Format");
        }

        public static bool IsLeapYear(int year)
        {
            throw new global::System.NotImplementedException("Date.IsLeapYear");
        }

        public bool IsSameDay(Date other)
        {
            throw new global::System.NotImplementedException("Date.IsSameDay");
        }

        public int Month()
        {
            ////throw new global::System.NotImplementedException("Date.Month");
            return date.Month;
        }

        public int MonthsBetween(Date other)
        {
            throw new global::System.NotImplementedException("Date.MonthsBetween");
        }

        public static Date NewInstance(int year, int month, int day)
        {
            ////throw new global::System.NotImplementedException("Date.NewInstance");
            return new Date(year, month, day);
        }

        public static Date Parse(string str)
        {
            ////throw new global::System.NotImplementedException("Date.Parse");
            return new Date(SysDateTime.Parse(str));
        }

        public Date ToStartOfMonth()
        {
            throw new global::System.NotImplementedException("Date.ToStartOfMonth");
        }

        public Date ToStartOfWeek()
        {
            throw new global::System.NotImplementedException("Date.ToStartOfWeek");
        }

        public static Date Today()
        {
            ////throw new global::System.NotImplementedException("Date.Today");
            return new Date(SysDateTime.Today);
        }

        public static Date ValueOf(object o)
        {
            throw new global::System.NotImplementedException("Date.ValueOf");
        }

        public static Date ValueOf(string str)
        {
            throw new global::System.NotImplementedException("Date.ValueOf");
        }

        public int Year()
        {
            ////throw new global::System.NotImplementedException("Date.Year");
            return date.Year;
        }
    }
}