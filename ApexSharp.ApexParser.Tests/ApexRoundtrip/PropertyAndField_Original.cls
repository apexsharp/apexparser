public class PropertyAndField
{
    public DateTime DateTimeGetSet { get; set; }
    public List<DateTime> DateTimeGetSetGeneric { get; set; }
    public Datetime[] DateTimeGetSetArray { get; set; }
    public Datetime DateTimeEmpty;
    public Datetime DateTimeInitialized = Datetime.Now();
    public List<DateTime> DateTimeList = new List<DateTime>();
    public DateTime[] DateTimeArray = new DateTime[5];
    public string Name = 'jay';
    public final string nameFinal = 'jay';
    public static string NameStatic = 'jay';
    public static final string NameStaticFinal = 'jay';
    
    public List<Contact> ContactList = [SELECT Id, Email FROM Contact];

    public Set<String> stringSet = new Set<String>{};

    public boolean shouldRedirect { get;set; }
    
    // not supported yet, see issue #8
    // {
    //     shouldRedirect = false;
    // }
    //
    public string[] newStringArray = new string[] {'Hi'};

    public void MethodOne()
    {
        Datetime dateTimeEmpty;
        dateTimeEmpty = Datetime.Now();
        Datetime dateTimeInitilized = Datetime.Now();
        List<Datetime> dateTimeList = new List<Datetime>();
        Datetime[] dateTimeArrary = new Datetime[5];
        string name;
        name = 'Jay';
    }
}