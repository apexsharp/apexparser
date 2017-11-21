using System;
using NUnit.Framework;
using Serilog;

namespace ApexSharpDemo
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

            Console.WriteLine("One Time Setup Got Called");
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            Console.WriteLine("Cleanup got called");
        }
    }
}
