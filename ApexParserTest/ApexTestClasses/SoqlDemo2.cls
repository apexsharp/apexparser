public with sharing abstract class SoqlDemo {

    public List<Contact> contactList = [SELECT Id, Email FROM Contact];

    public SoqlDemo()
    {
        contactList = [SELECT Id, Email FROM Contact];
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

        List<Contact> contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
        for (Contact c : contacts)
        {
            System.debug(c.Email); c.Email = 'new@new.com';
        }
        update contacts;

        contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
        for (Contact c : contacts)
        {
            System.debug(c.Email);
        }
        delete contacts;

        contacts = [SELECT Id, Email FROM Contact WHERE Id = :contactNew.Id];
        if (contacts.isEmpty())
        {
            System.debug('Delete Worked');
        }
    }

    public static void OneVsListDemo()
    {
        List<Contact> contacts = [SELECT Id, Email FROM Contact];

        Contact contact = [SELECT Id, Email FROM Contact LIMIT 1];
    }

    public static void VariableScope(Integer x)
    {
        if(x==5) {
            List<Contact> objectList;
            objectList = [SELECT Id FROM Contact];
        } else {
            Contact objectList;
            objectList = [SELECT Id FROM Contact];
        }
    }

    public static void MiscStatements()
    {
    	System.runAs([SELECT Id FROM Contact])
    	{
			try
			{
				while ([SELECT Id FROM Contact] != 0)
				{
					delete [SELECT Id FROM Contact];
				}
			}
			catch
			{
				for (Contact c : [SELECT Id, Name FROM Contact])
				{
					insert [SELECT Id FROM Contact];
				}
			}
			finally
			{
				do
				{
					for (Id id = [SELECT Id FROM Contact], Id id2 = [
						select id from Contact
						]; id < [SELECT Id FROM Contact]; 
						id += [SELECT Id FROM Contact], 
						id2 -= [SELECT Id FROM Contact])
					{
						update [SELECT Id FROM Contact];
					}
				}
				while ([SELECT Id FROM Contact] != 0);
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
			[SELECT ID, Name, BillingLatitude, BillingLongitude 
			FROM Account 
			WHERE DISTANCE(My_Location_Field__c, GEOLOCATION(10,10), :units) < 10]; 
    }
}
