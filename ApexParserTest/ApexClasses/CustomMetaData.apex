public class CustomMetaData {
public static void GetCustomMetaData() {
    CustomMetadataDemo__mdt[] threatMappings = [SELECT MasterLabel, QualifiedApiName,Name__c FROM CustomMetadataDemo__mdt];
    for (CustomMetadataDemo__mdt threatMapping : threatMappings) {
        System.debug(threatMapping.Name__c);
    }

}
}