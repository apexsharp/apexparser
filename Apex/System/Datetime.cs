namespace Apex.System
{
    using SysDateTime = global::System.DateTime;

    public class DateTime
    {
        private SysDateTime dateTime;

        private SysDateTime dateTimeGmt => dateTime.ToUniversalTime();

        internal DateTime(SysDateTime dt) => dateTime = dt;

        internal DateTime(int year, int month, int day)
        {
            dateTime = new SysDateTime(year, month, day);
        }

        internal DateTime(int year, int month, int day, int hour, int minute, int second)
        {
            dateTime = new SysDateTime(year, month, day, hour, minute, second);
        }

        public static DateTime Now()
        {
            return new DateTime(SysDateTime.Now);
        }

        public DateTime AddDays(int days)
        {
            ////throw new global::System.NotImplementedException("DateTime.AddDays");
            return new DateTime(dateTime.AddDays(days));
        }

        public void AddError(object msg)
        {
            throw new global::System.NotImplementedException("DateTime.AddError");
        }

        public void AddError(object msg, bool escape)
        {
            throw new global::System.NotImplementedException("DateTime.AddError");
        }

        public void AddError(string msg)
        {
            throw new global::System.NotImplementedException("DateTime.AddError");
        }

        public void AddError(string msg, bool escape)
        {
            throw new global::System.NotImplementedException("DateTime.AddError");
        }

        public DateTime AddHours(int hours)
        {
            ////throw new global::System.NotImplementedException("DateTime.AddHours");
            return new DateTime(dateTime.AddHours(hours));
        }

        public DateTime AddMinutes(int minutes)
        {
            ////throw new global::System.NotImplementedException("DateTime.AddMinutes");
            return new DateTime(dateTime.AddMinutes(minutes));
        }

        public DateTime AddMonths(int months)
        {
            ////throw new global::System.NotImplementedException("DateTime.AddMonths");
            return new DateTime(dateTime.AddMonths(months));
        }

        public DateTime AddSeconds(int seconds)
        {
            ////throw new global::System.NotImplementedException("DateTime.AddSeconds");
            return new DateTime(dateTime.AddSeconds(seconds));
        }

        public DateTime AddYears(int years)
        {
            ////throw new global::System.NotImplementedException("DateTime.AddYears");
            return new DateTime(dateTime.AddYears(years));
        }

        public Date Date()
        {
            ////throw new global::System.NotImplementedException("DateTime.Date");
            return new Date(dateTime.Date);
        }

        public Date DateGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.DateGmt");
            return new Date(dateTimeGmt);
        }

        public int Day()
        {
            ////throw new global::System.NotImplementedException("DateTime.Day");
            return dateTime.Day;
        }

        public int DayGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.DayGmt");
            return dateTimeGmt.Day;
        }

        public int DayOfYear()
        {
            ////throw new global::System.NotImplementedException("DateTime.DayOfYear");
            return dateTime.DayOfYear;
        }

        public int DayOfYearGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.DayOfYearGmt");
            return dateTimeGmt.DayOfYear;
        }

        public string Format()
        {
            throw new global::System.NotImplementedException("DateTime.Format");
        }

        public string Format(string dateformat)
        {
            throw new global::System.NotImplementedException("DateTime.Format");
        }

        public string Format(string dateformat, string timezone)
        {
            throw new global::System.NotImplementedException("DateTime.Format");
        }

        public string FormatGmt(string dateformat)
        {
            throw new global::System.NotImplementedException("DateTime.FormatGmt");
        }

        public string FormatLong()
        {
            throw new global::System.NotImplementedException("DateTime.FormatLong");
        }

        public long GetTime()
        {
            throw new global::System.NotImplementedException("DateTime.GetTime");
        }

        public int Hour()
        {
            ////throw new global::System.NotImplementedException("DateTime.Hour");
            return dateTime.Hour;
        }

        public int HourGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.HourGmt");
            return dateTimeGmt.Hour;
        }

        public bool IsSameDay(DateTime other)
        {
            throw new global::System.NotImplementedException("DateTime.IsSameDay");
        }

        public int Millisecond()
        {
            ////throw new global::System.NotImplementedException("DateTime.Millisecond");
            return dateTime.Millisecond;
        }

        public int MillisecondGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.MillisecondGmt");
            return dateTimeGmt.Millisecond; // doesn't make much sense
        }

        public int Minute()
        {
            ////throw new global::System.NotImplementedException("DateTime.Minute");
            return dateTime.Minute;
        }

        public int MinuteGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.MinuteGmt");
            return dateTimeGmt.Minute; // doesn't make much sense
        }

        public int Month()
        {
            ////throw new global::System.NotImplementedException("DateTime.Month");
            return dateTime.Month;
        }

        public int MonthGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.MonthGmt");
            return dateTimeGmt.Month;
        }

        public static DateTime NewInstance(Date date, Time time)
        {
            ////throw new global::System.NotImplementedException("DateTime.NewInstance");
            return new DateTime(date.date + time.time);
        }

        public static DateTime NewInstance(int year, int month, int day)
        {
            ////throw new global::System.NotImplementedException("DateTime.NewInstance");
            return new DateTime(year, month, day);
        }

        public static DateTime NewInstance(int year, int month, int day, int hour, int minute, int second)
        {
            ////throw new global::System.NotImplementedException("DateTime.NewInstance");
            return new DateTime(year, month, day, hour, minute, second);
        }

        public static DateTime NewInstance(long time)
        {
            throw new global::System.NotImplementedException("DateTime.NewInstance");
        }

        public static DateTime NewInstanceGmt(Date date, Time time)
        {
            throw new global::System.NotImplementedException("DateTime.NewInstanceGmt");
        }

        public static DateTime NewInstanceGmt(int year, int month, int day)
        {
            throw new global::System.NotImplementedException("DateTime.NewInstanceGmt");
        }

        public static DateTime NewInstanceGmt(int year, int month, int day, int hour, int minute, int second)
        {
            throw new global::System.NotImplementedException("DateTime.NewInstanceGmt");
        }

        public static DateTime Parse(string str)
        {
            ////throw new global::System.NotImplementedException("DateTime.Parse");
            return new DateTime(SysDateTime.Parse(str));
        }

        public int Second()
        {
            ////throw new global::System.NotImplementedException("DateTime.Second");
            return dateTime.Second;
        }

        public int SecondGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.SecondGmt");
            return dateTimeGmt.Second;
        }

        public Time Time()
        {
            ////throw new global::System.NotImplementedException("DateTime.Time");
            return new Time(dateTime.TimeOfDay);
        }

        public Time TimeGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.TimeGmt");
            return new Time(dateTimeGmt.TimeOfDay);
        }

        public static DateTime ValueOf(object o)
        {
            throw new global::System.NotImplementedException("DateTime.ValueOf");
        }

        public static DateTime ValueOf(string str)
        {
            throw new global::System.NotImplementedException("DateTime.ValueOf");
        }

        public static DateTime ValueOfGmt(string str)
        {
            throw new global::System.NotImplementedException("DateTime.ValueOfGmt");
        }

        public int Year()
        {
            ////throw new global::System.NotImplementedException("DateTime.Year");
            return dateTime.Year;
        }

        public int YearGmt()
        {
            ////throw new global::System.NotImplementedException("DateTime.YearGmt");
            return dateTimeGmt.Year;
        }
    }
}