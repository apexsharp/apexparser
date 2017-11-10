public with sharing abstract class SoqlDemo {

    public List<Contact> contactList = [SELECT 1, Id, Email FROM Contact];

    public SoqlDemo()
    {
        contactList = [SELECT 2, Id, Email FROM Contact];
    }

    public abstract void AbstractMethod();


    public void CommentedQueryIsIgnored()
    {
    	// Contact contact = [SELECT Id, Email FROM Contact LIMIT 1];

    	/* 
        	List<Contact> contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
    	*/
    }

    /**
     * A simple CRUD Example
     */
    public static void CrudExample()
    {
        Contact contactNew = new Contact(LastName = 'Jay', EMail = 'abc@abc.com');
        insert contactNew;

        System.debug(contactNew.Id);

        List<Contact> contacts = [SELECT 3, Id, Email FROM Contact WHERE Id = :contactNew.Id];
        for (Contact c : contacts)
        {
            System.debug(c.Email); c.Email = 'new@new.com';
        }
        update contacts;

        contacts = [SELECT 4, Id, Email FROM Contact WHERE Id = :contactNew.Id];
        for (Contact c : contacts)
        {
            System.debug(c.Email);
        }
        delete contacts;

        contacts = [SELECT 5, Id, Email FROM Contact WHERE Id = :contactNew.Id];
        if (contacts.isEmpty())
        {
            System.debug('Delete Worked');
        }
    }

    public static void OneVsListDemo()
    {
        List<Contact> contacts = [SELECT 6, Id, Email FROM Contact];

        Contact contact = [SELECT 7, Id, Email FROM Contact LIMIT 1];
    }

    public static void VariableScope(Integer x)
    {
        if(x==5) {
            List<Contact> objectList;
            objectList = [SELECT 8, Id FROM Contact];
        } else {
            Contact objectList;
            objectList = [SELECT 9, Id FROM Contact];
        }
    }

    public static void MiscStatements()
    {
    	System.runAs([SELECT 10, Id FROM Contact])
    	{
			try
			{
				while ([SELECT 11, Id FROM Contact] != 0)
				{
					delete [SELECT 12, Id FROM Contact];
				}
			}
			catch
			{
				for (Contact c : [SELECT 13, Id, Name FROM Contact])
				{
					insert [SELECT 14, Id FROM Contact];
				}
			}
			finally
			{
				do
				{
					for (Id id = [SELECT 15, Id FROM Contact], Id id2 = [
						select 16, id from Contact
						]; id < [SELECT 17, Id FROM Contact]; 
						id += [SELECT 18, Id FROM Contact], 
						id2 -= [SELECT 19, Id FROM Contact])
					{
						update [SELECT 20, Id FROM Contact];
					}
				}
				while ([SELECT 21, Id FROM Contact] != 0);
			}
		}
    }

    public static void FindQuery()
    {
		List<List<SObject>> searchList = 
			[FIND :myString1 IN ALL FIELDS 
			RETURNING 
				Account (Id, Name WHERE Name LIKE :myString2
						LIMIT :myInt3), 
				Contact, 
				Opportunity, 
				Lead 
			WITH DIVISION =:myString4 
			LIMIT :myInt5];
    }

    public void MultilineQuery()
    {
		String units = 'mi';
		List<Account> accountList = 
			[SELECT 23, ID, Name, BillingLatitude, BillingLongitude 
			FROM Account 
			WHERE DISTANCE(My_Location_Field__c, GEOLOCATION(10,10), :units) < 10]; 
    }
}
