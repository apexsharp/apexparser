namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Scontrol : SObject
	{
		public string Name {set;get;}
		public string DeveloperName {set;get;}
		public string Description {set;get;}
		public string EncodingKey {set;get;}
		public string HtmlWrapper {set;get;}
		public string Filename {set;get;}
		public int BodyLength {set;get;}
		public string Binary {set;get;}
		public string ContentSource {set;get;}
		public bool SupportsCaching {set;get;}
		public string NamespacePrefix {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
