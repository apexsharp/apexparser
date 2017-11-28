public class PropertyAndField
{
    public DateTime DateTimeGetSet { get; set; }
    public List<DateTime> DateTimeGetSetGeneric { get; set; }
    public DateTime[] DateTimeGetSetArray { get; set; }
    public DateTime DateTimeEmpty;
    public DateTime DateTimeInitialized = DateTime.Now();
    public List<DateTime> DateTimeList = new List<DateTime>();
    public DateTime[] DateTimeArray = new DateTime[5];
    public string Name = 'jay';
    public final string nameFinal = 'jay';
    public static string NameStatic = 'jay';
    public static final string NameStaticFinal = 'jay';
    
    public List<Contact> ContactList = [SELECT Id, Email FROM Contact];

    public Set<String> stringSet = new Set<String>{};

    public boolean shouldRedirect { get;set; }
    {
        shouldRedirect =false;
    }

    public string[] newStringArray = new string[] {'Hi'};

    public void MethodOne()
    {
        DateTime dateTimeEmpty;
        dateTimeEmpty = DateTime.Now();
        DateTime dateTimeInitilized = DateTime.Now();
        List<DateTime> dateTimeList = new List<DateTime>();
        DateTime[] dateTimeArrary = new DateTime[5];
        string name;
        name = 'Jay';
    }
}