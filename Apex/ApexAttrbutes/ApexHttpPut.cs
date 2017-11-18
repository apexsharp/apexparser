using System;

namespace Apex.ApexAttrbutes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpDelete : global::System.Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPost : global::System.Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGet : global::System.Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPatch : global::System.Attribute
    {
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPut : global::System.Attribute
    {
    }
}