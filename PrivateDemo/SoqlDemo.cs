namespace PrivateDemo
{
    using System;
    using ApexSharpApi;
    using Serilog;

    public class SoqlDemo
    {
        public static void CrudExample()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

            // Contact contactNew = new Contact() { LastName = "Jay", Email = "jay@jay.com" };
            //   var soql = SoqlApi.ConvertSoql(
            //       "SELECT ID, Email, Name FROM Contact WHERE EMail =  && LastName = :contactNew.LastName && Number == :contactNew.Number LIMIT 1",
            //       contactNew.Email, contactNew.LastName);
            //  Console.WriteLine(soql);
        }
    }
}
