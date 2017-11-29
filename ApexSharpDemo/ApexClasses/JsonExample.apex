public class JsonExample
{
    public void JsonExampleMethod()
    {
        Contact contact = new Contact();
        contact.LastName = 'Jay';
        contact.Email = 'jay@jay.com';

        String jsonString = JSON.Serialize(contact);
        Contact newContact = (Contact)JSON.Deserialize(jsonString, String.class);
    }
}