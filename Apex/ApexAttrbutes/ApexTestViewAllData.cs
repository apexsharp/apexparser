using System;

namespace Apex.ApexAttrbutes.ApexSharpBase.Attribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class ApexTestViewAllData : global::System.Attribute
    {
        public ApexTestViewAllData(bool isSharing)
        {
            IsSharing = isSharing;
        }

        protected virtual bool IsSharing { get; }
    }
}