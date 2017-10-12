namespace ApexSharpDemo.ApexCode
{
    using Apex.System;

    public class PropertyAndField
    {
        //    transient Integer TransientInteger;
        public DateTime DateTimeGetSet { get; set; }
        public List<DateTime> DateTimeGetSetGeneric { get; set; }
        public DateTime[] DateTimeGetSetArray { get; set; }
        public DateTime DateTimeEmpty;
        public DateTime DateTimeInitialized = DateTime.Now();
        public List<DateTime> DateTimeList = new List<DateTime>();
        public DateTime[] DateTimeArray = new DateTime[5];
        public string Name = "Jay";
        //public final String nameFinal = 'jay';
        public static string NameStatic = "Jay";
        // public static final String NameStaticFinal = 'jay';

        public void MethodOne()
        {
            DateTime dateTimeEmpty;
            dateTimeEmpty = DateTime.Now();
            DateTime dateTimeInitilized = DateTime.Now();
            List<DateTime> dateTimeList = new List<DateTime>();
            DateTime[] dateTimeArrary = new DateTime[5];
            string name;
            name = "Jay";
        }
    }
}
