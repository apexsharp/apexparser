namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AuraDefinitionBundleInfo : SObject
	{
		public string DurableId {set;get;}
		public string AuraDefinitionBundleId {set;get;}
		public double ApiVersion {set;get;}
		public string DeveloperName {set;get;}
		public string NamespacePrefix {set;get;}
	}
}
