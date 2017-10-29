using Apex.System;

namespace ApexSharpDemo.ApexCode
{
    public class JsonExample
    {
        public string JsonExampleMethod()
        {
            string jsonString = JSON.Serialize("Hello World");
            string newName = JSON.Deserialize<string>(jsonString);
            return newName;
        }
    }
}
