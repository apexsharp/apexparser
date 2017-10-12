using Apex.System;

namespace ApexSharpDemo.ApexCode
{
    public class JsonExample
    {
        public void JsonExampleMethod()
        {
            string name = "Jay";

            string jsonString = JSON.Serialize(name);
            string newName = JSON.Deserialize<string>(jsonString);
        }
    }
}
