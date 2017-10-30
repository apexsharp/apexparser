namespace ApexSharpDemo.ApexCode
{
    using ApexSharpBase.Attribute;

  //  [Route("api/v1/restdemo")]
    public class ClassRest
    {
      //  [HttpDelete]
        [ApexGlobel]
        public static void DoDelete()
        {
        }

      //  [HttpPost]
        [ApexGlobel]
        public static void Post()
        {
        }

     //   [HttpGet]
        [ApexGlobel]
        public static string Get()
        {
            return "Jay";
        }

   //     [HttpPatch]
        [ApexGlobel]
        public static void Patch()
        {
        }

      //  [HttpPut]
        [ApexGlobel]
        public static void Put()
        {
        }
    }
}
