@IsTest
private class DxTestThree
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
        // Dx.updatePhone('jay@jay.com', '555-1212');
    }
}
