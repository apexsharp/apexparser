using Apex.System;
using SalesForceAPI.Apex;

namespace ApexSharpDemo.SObjects
{
    public class UserLicense : SObject
    {
        public string Id { set; get; }
        public string LicenseDefinitionKey { set; get; }
        public string Name { set; get; }
        public int MonthlyLoginsUsed { set; get; }
        public int MonthlyLoginsEntitlement { set; get; }
        public DateTime SystemModstamp { set; get; }
    }
}