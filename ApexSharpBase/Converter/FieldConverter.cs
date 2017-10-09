using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexSharpBase.Converter
{
    public class FieldConverter
    {
        public class Type
        {
            public Type(string apexType, string cSharpType)
            {
                ApexType = apexType;
                CSharpType = cSharpType;
            }
            public string ApexType { get; set; }
            public string CSharpType { get; set; }
        }

        public static List<Type> TypeList = new List<Type>()
        {
            new Type("Integer", "int"),
            new Type("String", "string"),
            new Type("Boolean", "bool")
        };

        public static string GetApexTypes(string cSharpType)
        {
            Type type = TypeList.FirstOrDefault(x => x.CSharpType.Equals(cSharpType));
            return type?.ApexType;
        }

        public static string GetCSharpTypes(string apexType)
        {
            Type type = TypeList.FirstOrDefault(x => x.ApexType.Equals(apexType));
            return type?.CSharpType;
        }

    }
}
