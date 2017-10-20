using System;

namespace SalesForceAPI.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldType : System.Attribute
    {
        public FieldType(string fieldLengthAttribute)
        {
            FieldLengthAttribute = fieldLengthAttribute;
        }

        public string FieldLengthAttribute { get; set; }
    }
}