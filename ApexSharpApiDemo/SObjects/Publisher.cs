namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Publisher : SObject
	{
		public string DurableId {set;get;}
		public string Name {set;get;}
		public string NamespacePrefix {set;get;}
		public bool IsSalesforce {set;get;}
		public int MajorVersion {set;get;}
		public int MinorVersion {set;get;}
	}
}
