public class Demo
{
    public Contact contact { get; set; }

    public Demo()
    {
        contact = new Contact();
    }

    public PageReference Save()
    {
        try
        {
            insert contact;
        }
        catch (DmlException e)
        {
            ApexPages.AddMessages(e);
        }

        return null;
    }

    public static string UpdatePhone(string email, string newPhone)
    {
        List<Contact> contacts = GetContactByEMail(email);
        if (contacts.IsEmpty())
        {
            return 'Not Found';
        }
        else
        {
            contacts[0].Phone = newPhone;
            UpdateContacts(contacts);
            return 'Phone Number Updated';
        }
    }

    public static List<Contact> GetContactByEMail(string email)
    {
        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact WHERE Email = :email];
        return contacts;
    }

    public static List<Contact> GetContacts()
    {
        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact];
        return contacts;
    }

    public static void UpdateContacts(List<Contact> contacts)
    {
        update contacts;
    }
}