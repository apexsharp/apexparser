@IsTest
private class DxTestFour
{
    @TestSetup
    public static void setup()
    {
        Contact contactNew = new Contact();
        contactNew.LastName = 'Jay';
        contactNew.Email = 'jay@jay.com';
        insert contactNew;
    }

    @IsTest
    public static void updatePhoneTestValidEmail()
    {
        String Dx = 'This should not work';
    }
}
