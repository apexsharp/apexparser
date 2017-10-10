using SalesForceAPI.Apex;
using SalesForceAPI.ApexApi;

namespace ApexSharpDemo.ApexCode
{

    using Apex.System;
    public class Json
    {
        public Json()
        {
            SObject responseDto = new SObject();

            string jsonString = JSON.Serialize(responseDto);

            responseDto = JSON.Deserialize<SObject>(jsonString);
        }
    }
}
