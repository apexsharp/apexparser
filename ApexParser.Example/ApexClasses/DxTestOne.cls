Private Class DxTestOne
{
    Public Static void setup()
    {
        Contact contactNew = new Contact();
        contactNew.LastName = 'Jay';
        contactNew.Email = 'jay@jay.com';
        insert contactNew;
    }
}
