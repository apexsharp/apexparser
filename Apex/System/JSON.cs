using SysType = System.Type;

namespace Apex.System
{
    public class JSON
    {
        public JSON()
        {
            throw new global::System.NotImplementedException("JSON");
        }

        public static string Serialize(object o)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(o);
        }

        public static T Deserialize<T>(string jsonString)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static object Deserialize(string jsonString, SysType type)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString, type);
        }

        public object Clone()
        {
            throw new global::System.NotImplementedException("JSON.Clone");
        }

        public static JSONGenerator CreateGenerator(bool pretty)
        {
            throw new global::System.NotImplementedException("JSON.CreateGenerator");
        }

        public static JSONParser CreateParser(string jsonString)
        {
            throw new global::System.NotImplementedException("JSON.CreateParser");
        }


        public static object DeserializeStrict(string jsonString, Type apexType)
        {
            throw new global::System.NotImplementedException("JSON.DeserializeStrict");
        }

        public static object DeserializeUntyped(string jsonString)
        {
            throw new global::System.NotImplementedException("JSON.DeserializeUntyped");
        }



        public static string Serialize(object o, bool suppressApexObjectNulls)
        {
            throw new global::System.NotImplementedException("JSON.Serialize");
        }

        public static string SerializePretty(object o)
        {
            throw new global::System.NotImplementedException("JSON.SerializePretty");
        }

        public static string SerializePretty(object o, bool suppressApexObjectNulls)
        {
            throw new global::System.NotImplementedException("JSON.SerializePretty");
        }
    }
}