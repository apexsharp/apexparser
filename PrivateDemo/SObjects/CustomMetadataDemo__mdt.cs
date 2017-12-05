namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CustomMetadataDemo__mdt : SObject
	{
		public string DeveloperName {set;get;}

		public string MasterLabel {set;get;}

		public string Language {set;get;}

		public string NamespacePrefix {set;get;}

		public string Label {set;get;}

		public string QualifiedApiName {set;get;}

		public string Name__c {set;get;}
	}
}
