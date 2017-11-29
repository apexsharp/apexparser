namespace Apex.System
{
    using TimeSpan = global::System.TimeSpan;

    public class Time
    {
        internal TimeSpan time;

        internal Time(TimeSpan ts) => time = ts;

        public void AddError(object msg)
        {
            throw new global::System.NotImplementedException("Time.AddError");
        }

        public void AddError(object msg, bool escape)
        {
            throw new global::System.NotImplementedException("Time.AddError");
        }

        public void AddError(string msg)
        {
            throw new global::System.NotImplementedException("Time.AddError");
        }

        public void AddError(string msg, bool escape)
        {
            throw new global::System.NotImplementedException("Time.AddError");
        }

        public Time AddHours(int hours)
        {
            ////throw new global::System.NotImplementedException("Time.AddHours");
            return new Time(time + TimeSpan.FromHours(hours));
        }

        public Time AddMilliseconds(int milliseconds)
        {
            ////throw new global::System.NotImplementedException("Time.AddMilliseconds");
            return new Time(time + TimeSpan.FromMilliseconds(milliseconds));
        }

        public Time AddMinutes(int minutes)
        {
            ////throw new global::System.NotImplementedException("Time.AddMinutes");
            return new Time(time + TimeSpan.FromMinutes(minutes));
        }

        public Time AddSeconds(int seconds)
        {
            ////throw new global::System.NotImplementedException("Time.AddSeconds");
            return new Time(time + TimeSpan.FromSeconds(seconds));
        }

        public int Hour()
        {
            ////throw new global::System.NotImplementedException("Time.Hour");
            return time.Hours;
        }

        public int Millisecond()
        {
            ////throw new global::System.NotImplementedException("Time.Millisecond");
            return time.Milliseconds;
        }

        public int Minute()
        {
            ////throw new global::System.NotImplementedException("Time.Minute");
            return time.Minutes;
        }

        public static Time NewInstance(int hour, int minute, int second, int millisecond)
        {
            ////throw new global::System.NotImplementedException("Time.NewInstance");
            return new Time(new TimeSpan(hour, minute, second) + TimeSpan.FromMilliseconds(millisecond));
        }

        public int Second()
        {
            ////throw new global::System.NotImplementedException("Time.Second");
            return time.Seconds;
        }
    }
}