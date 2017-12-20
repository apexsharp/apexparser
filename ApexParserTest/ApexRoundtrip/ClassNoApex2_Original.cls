public class ClassNoApex {
    // Any classes in NoApex name space will be commented out in Apex and uncommented on c#.
    public static void MethodOne()
    {
        //:NoApex NoApex.Serilog.LogInfo("Jay");
    }

    public static List<Contact> getContacts()
    {
        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact];
        //:NoApex NoApex.Serilog.LogInfo(contacts.size().ToString());
        return contacts;
    }

    public static void callingNonApexCode()
    {
        //:NoApex NoApex.Serilog.LogInfo("Hi");
    }

    // Any method in NoApex name space will be commented out in Apex and uncommented on c#.
    //:NoApex public static void NoApexMethodTwo()
    //:NoApex {
    //:NoApex     NoApex.Serilog.LogInfo("Jay");
    //:NoApex }
}