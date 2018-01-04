namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.ApexSharp.ApexAttributes;
    using Apex.System;
    using SObjects;

    public class PropertyAndField
    {
        public Datetime DateTimeGetSet { get; set; }

        public List<Datetime> DateTimeGetSetGeneric { get; set; }

        public Datetime[] DateTimeGetSetArray { get; set; }

        public Datetime DateTimeEmpty;

        public Datetime DateTimeInitialized = Datetime.Now();

        public List<Datetime> DateTimeList = new List<Datetime>();

        public Datetime[] DateTimeArray = new Datetime[5];

        public string Name = "jay";

        public readonly string nameFinal = "jay";

        public static string NameStatic = "jay";

        public static readonly string NameStaticFinal = "jay";

        public List<Contact> ContactList = Soql.query<Contact>(@"SELECT Id, Email FROM Contact");

        public Set<string> stringSet = new Set<string>{};

        public bool shouldRedirect { get; set; }

        // not supported yet, see issue #8
        // {
        //     shouldRedirect = false;
        // }
        //
        public string[] newStringArray = new string[]{"Hi"};

        public void MethodOne()
        {
            Datetime dateTimeEmpty;
            dateTimeEmpty = Datetime.Now();
            Datetime dateTimeInitilized = Datetime.Now();
            List<Datetime> dateTimeList = new List<Datetime>();
            Datetime[] dateTimeArrary = new Datetime[5];
            string name;
            name = "Jay";
        }
    }
}
