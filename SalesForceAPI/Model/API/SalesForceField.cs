namespace SalesForceAPI.Model.API
{
    public class SalesForceField
    {
        public SalesForceField(string name, string type, int lentgh)
        {
            FieldType = type;
            FieldName = name;
            FieldLength = lentgh;
        }

        public string FieldName { get; set; }
        public int FieldLength { get; set; }
        public string FieldType { get; set; }
    }
}