using System;

namespace Apex.ApexSharp.Attribute
{
    public class ApexSharpAttribute
    {
    }


    [AttributeUsage(AttributeTargets.All)]
    public class ApexGlobel : global::System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ApexRest : global::System.Attribute
    {
        public ApexRest(string url)
        {
            Url = url;
        }

        public virtual string Url { get; }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class ApexHttpPut : global::System.Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Class)]
    public class ApexWithSharing : global::System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ApexWithOutSharing : global::System.Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class ApexTestViewAllData : global::System.Attribute
    {
        public ApexTestViewAllData(bool isSharing)
        {
            IsSharing = isSharing;
        }

        public virtual bool IsSharing { get; }
    }
}