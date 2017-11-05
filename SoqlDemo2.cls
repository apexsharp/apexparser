public with sharing abstract class SoqlDemo {

    public List<Contact> contactList = [SELECT Id, Email FROM Contact];

    public SoqlDemo()
    {
        contactList = [SELECT Id, Email FROM Contact];
    }

    public abstract void AbstractMethod();

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

    // Advanced SOQL query demo from the official documentation:
    // https://developer.salesforce.com/docs/atlas.en-us.apexcode.meta/apexcode/langCon_apex_SOQL_variables.htm
    public static void UseApexVariablesInSoqlQueries()
    {
		Account A = new Account(Name='xxx');
		insert A;
		Account B;

		// A simple bind
		B = [SELECT Id FROM Account WHERE Id = :A.Id];

		// A bind with arithmetic
		B = [SELECT Id FROM Account 
			WHERE Name = :('x' + 'xx')];

		String s = 'XXX';

		// A bind with expressions
		B = [SELECT Id FROM Account 
			WHERE Name = :'XXXX'.substring(0,3)];

		// A bind with an expression that is itself a query result
		B = [SELECT Id FROM Account
			WHERE Name = :[SELECT Name FROM Account
							WHERE Id = :A.Id].Name];

		Contact C = new Contact(LastName='xxx', AccountId=A.Id);
		insert new Contact[]{C, new Contact(LastName='yyy', 
											accountId=A.id)};

		// Binds in both the parent and aggregate queries
		B = [SELECT Id, (SELECT Id FROM Contacts
						WHERE Id = :C.Id)
			FROM Account
			WHERE Id = :A.Id];

		// One contact returned
		Contact D = B.Contacts;

		// A limit bind
		Integer i = 1;
		B = [SELECT Id FROM Account LIMIT :i];

		// An OFFSET bind
		Integer offsetVal = 10;
		List<Account> offsetList = [SELECT Id FROM Account OFFSET :offsetVal];

		// An IN-bind with an Id list. Note that a list of sObjects
		// can also be used--the Ids of the objects are used for 
		// the bind
		Contact[] cc = [SELECT Id FROM Contact LIMIT 2];
		Task[] tt = [SELECT Id FROM Task WHERE WhoId IN :cc];

		// An IN-bind with a String list
		String[] ss = new String[]{'a', 'b'};
		Account[] aa = [SELECT Id FROM Account 
						WHERE AccountNumber IN :ss];

		// A SOSL query with binds in all possible clauses

		String myString1 = 'aaa';
		String myString2 = 'bbb';
		Integer myInt3 = 11;
		String myString4 = 'ccc';
		Integer myInt5 = 22;

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

    public void InvalidQueryExample()
    {
		String units = 'mi';
		List<Account> accountList = 
			[SELECT ID, Name, BillingLatitude, BillingLongitude 
			FROM Account 
			WHERE DISTANCE(My_Location_Field__c, GEOLOCATION(10,10), :units) < 10]; 
    }
}
