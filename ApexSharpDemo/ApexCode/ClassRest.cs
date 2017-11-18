
namespace ApexSharpDemo.ApexCode
{
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;
    using Apex.ApexAttributes;

    [RestResource("/api/v1/RestDemo")]
    [Global]
    public class ClassRest
    {
        [HttpDelete]
        [Global]
        public static void DoDelete()
        {
        }

        [HttpPost]
        [Global]
        public static void Post()
        {
        }

        [HttpGet]
        [Global]
        public static string Get()
        {
            return "Jay";
        }

        [HttpPatch]
        [Global]
        public static void Patch()
        {
        }

        [HttpPut]
        [Global]
        public static void Put()
        {
        }
    }
}
