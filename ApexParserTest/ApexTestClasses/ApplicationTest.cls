@IsTest
private class ApplicationTest 
{
	@IsTest
	private static void callingDomainFactoryShouldGiveRegisteredImplsAndMocks()
	{
		domainObjectAcct = 
			Domain.newInstance(
				new List<SObject> 
					{ new Account( 
						Id = testAccountId,
						Name = 'Test Account') }
				, Account.SObjectType);

		try {
			Domain.newInstance(new List<Contact>{ new Contact(LastName = 'TestContactLName') });
			System.assert(false, 'Expected exception');
		} catch (System.TypeException e) {
			System.assert(Pattern.Matches('Invalid conversion from runtime type \\w*\\.?fflib_ApplicationTest\\.ContactsConstructor to \\w*\\.?fflib_SObjectDomain\\.IConstructable',
				e.getMessage()), 'Exception message did not match the expected pattern: ' + e.getMessage());
		}	
	}
}