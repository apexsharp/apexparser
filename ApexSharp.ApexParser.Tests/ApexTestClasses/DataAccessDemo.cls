public with sharing class DataAccessDemo
{
    private DataAccessLayerI dl;

    public DataAccessDemo()
    {
        if (Test.isRunningTest())
        {
            dl = (DataAccessLayerMok) MokProvider.createMokProvidor(DataAccessLayerMok.class);
        }
        else
        {
            dl = new DataAccessLayer();
        }
    }

    public String UpdateContactEmailAddress(string firstName, string lastName, string email)
    {
        Contact contact = dl.GetContactByName(firstName, lastName);
        if (contact != null)
        {
            contact.Email = email;
            dl.UpdateContact(contact);
            return 'OK';
        }
        else
        {
            return 'FAIL';
        }
    }
}