namespace ApexParserTest.CSharpClasses
{
    using Apex.ApexAttributes;
    using Apex.ApexSharp;
    using Apex.System;
    using SObjects;

    public class CustomMetaData
    {
        public static void GetCustomMetaData()
        {
            CustomMetadataDemo__mdt[] threatMappings = Soql.Query<CustomMetadataDemo__mdt>("SELECT MasterLabel, QualifiedApiName,Name__c FROM CustomMetadataDemo__mdt");
            foreach (CustomMetadataDemo__mdt threatMapping in threatMappings)
            {
                System.debug(threatMapping.Name__c);
            }
        }
    }
}
