using System;

namespace Apex.ApexAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpDeleteAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPostAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class HttpGetAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPatchAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPutAttribute : Attribute
    {
    }
}