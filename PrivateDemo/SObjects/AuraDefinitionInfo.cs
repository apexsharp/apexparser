namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AuraDefinitionInfo : SObject
	{
		public string DurableId {set;get;}

		public string AuraDefinitionBundleInfoId {set;get;}

		public string AuraDefinitionId {set;get;}

		public string DefType {set;get;}

		public string Format {set;get;}

		public string Source {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string DeveloperName {set;get;}

		public string NamespacePrefix {set;get;}
	}
}
