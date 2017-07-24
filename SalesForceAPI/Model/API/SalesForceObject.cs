using System.Collections.Generic;

namespace SalesForceAPI.Model.API
{
    public class SalesForceObject
    {
        public List<SalesForceField> SalesForceFieldList = new List<SalesForceField>();
        public string ObjectName { get; set; }
        public SObjectDescribe SalesForceMetaDataObjectModel { get; set; }
    }
}