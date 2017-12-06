using System;
using ApexSharpApi;
using NUnit.Framework;

namespace Demo
{
    [SetUpFixture]
    public class OneTimeSetUp
    {
        [OneTimeSetUp]
        public static void Init()
        {
            // Always Initialize your settings before using it.
            Setup.Init();
            UnitTestDataManager.UnitTestDataManagerOn();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            UnitTestDataManager.UnitTestDataManagerOff();
        }
    }
}
