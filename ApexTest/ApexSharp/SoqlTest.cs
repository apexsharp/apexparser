using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apex.ApexSharp;
using NUnit.Framework;
using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;


namespace ApexTest.ApexSharp
{
    [TestFixture]
    class SoqlTest
    {
      
        public void QueryTest()
        {

            var apexSharp = new Apex.ApexSharp.ApexSharp();

            // Setup connection info
            apexSharp.SalesForceUrl("https://login.salesforce.com")
               .UseSalesForceApiVersion(40)
               .WithUserId("apexsharp@jayonsoftware.com")
               .AndPassword("9cHgyTpoRFuy8sv92ox9ycLAX")
               .AndToken("1v0EGMfR0NTkbmyQ2Jk4082PA")
               .SetApexFileLocation(@"C:\Dev\ApexSharp\src\classes\")
               .SetLogLevel(LogLevel.Info)
               .SaveApexSharpConfig(@"C:\Dev\ApexSharp\src\classes\ConfigInfo.json");

            apexSharp.Connect();




            //List<SObject> contacts = Soql.Query<SObject>("SELECT Id, Email, Name FROM Contact WHERE Id = :contactNewId LIMIT 1", new { contactNewId });

            List<SObject> contacts = SOQL.Query<SObject>("SELECT Id, Email, Name FROM Contact LIMIT 1");

            Console.WriteLine(contacts.Count);
        }
    }
}
