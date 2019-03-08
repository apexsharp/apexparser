public with sharing class Dx
{
    public static String updatePhone(String email, String newPhone)
    {
        List<Contact> contacts = getContactByEMail(email);
        if (contacts.isEmpty())
        {
            return 'Not Found';
        }
        else
        {
            contacts[0].Phone = newPhone;
            updateContacts(contacts);
            return 'Phone Number Updated';
        }
    }

    public static List<Contact> getContactByEMail(String email)
    {
        List<Contact> contacts = [SELECT Id, Email, Phone FROM Contact WHERE Email = :email];
        return contacts;
    }

    public static void updateContacts(List<Contact> contacts)
    {
        update contacts;
    }

    public String updatePhoneNonStatic(String email, String newPhone)
    {
        List<Contact> contacts = getContactByEMail(email);
        if (contacts.isEmpty())
        {
            return 'Not Found';
        }
        else
        {
            contacts[0].Phone = newPhone;
            update contacts;
            return 'Phone Number Updated';
        }
    }
}
