using NUnit.Framework;
using Serilog;

namespace SalesForceAPI.UnitTest
{
    [SetUpFixture]
    public class TestSetup
    {
        [OneTimeSetUp]
        public static void Init()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            ConnectionUtil.Session = new ApexSharp().SalesForceUrl("https://login.salesforce.com/")
                .AndSalesForceApiVersion(40)
                .WithUserId("apexsharp@jayonsoftware.com")
                .AndPassword("1v0EGMfR0NTkbmyQ2Jk4082PA")
                .AndToken("LUTAPwQstOZj9ESx7ghiLB1Ww")
                .CacheLocation(@"C:\DevSharp\ApexSharp\PrivateDemo\")
                .SaveConfigAt(@"C:\DevSharp\ApexSharp\PrivateDemo\config.json")
                .CreateSession();

            UnitTestDataManager.UnitTestDataManagerOn();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            UnitTestDataManager.UnitTestDataManagerOff();
        }
    }
}
