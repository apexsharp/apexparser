
namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using Apex.ApexAttrbutes;

    [RestResource("/api/v1/RestDemo")]
    [Globel]
    public class ClassRest
    {
        [HttpDelete]
        [Globel]
        public static void DoDelete()
        {
        }

        [HttpPost]
        [Globel]
        public static void Post()
        {
        }

        [HttpGet]
        [Globel]
        public static string Get()
        {
            return "Jay";
        }

        [HttpPatch]
        [Globel]
        public static void Patch()
        {
        }

        [HttpPut]
        [Globel]
        public static void Put()
        {
        }
    }
}
