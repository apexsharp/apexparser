using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;

namespace ApexSharpDemo.ApexCode
{

    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;


    public class SoqlAndDml
    {

        public static void RunContactDemo()
        {
            string newEmail = "Jay@JayOnSoftware.Com";

            List<Contact> listOfContact = SOQL.Query<Contact>("SELECT Id, Email FROM Contact LIMIT 1");

            if (listOfContact.Size() == 1)
            {
                System.Debug(listOfContact[0].Email);

                listOfContact[0].Email = newEmail;

                SOQL.Update(listOfContact[0]);

                listOfContact = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :newEmail LIMIT 1", new { newEmail });

                System.Debug(listOfContact[0].Email);
            }
            else
            {
                System.Debug("No Records Found in the Contact Object Matching " + newEmail);
            }
        }


        public void DeleteRecord()
        {
            string eMail = "jay@jayonsoftware.com";
            List<Contact> listOfContact =
                SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :eMail LIMIT 1", new { eMail });
            if (listOfContact.Size() == 1)
            {
                System.Debug(listOfContact[0].Email);

                SOQL.Delete(listOfContact);
            }
        }


        public void MasterChildLookup()
        {
            List<Contact> listOfContact = SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact LIMIT 1");
            if (listOfContact.Size() == 1)
            {
                System.Debug(listOfContact[0].Email);
            }
        }


        public void PassingSoqlVariabls()
        {
            string eMail = "jay@jayonsoftware.com";
            List<Contact> listOfContact =
                SOQL.Query<Contact>("SELECT Id, Email, Name FROM Contact WHERE EMail = :eMail LIMIT 1", new { eMail });
            if (listOfContact.Size() == 1)
            {
                Apex.System.System.Debug(listOfContact[0].Email);
            }
        }

        public void SoqlLookup()
        {
            Contact contact = new Contact();
            contact.LastName = "Jay";
            contact.Email = "jay@jayonsoftware.com";

            SOQL.Insert(contact);


            List<Contact> listOfContact = SOQL.Query<Contact>("SELECT Id, Email, LastName FROM Contact WHERE LastName = 'Jay'");

            System.Debug(listOfContact.Size());

            if (listOfContact.Size() == 1)
            {
                System.Debug(listOfContact[0].Email);
            }
        }
    }
}