namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexClass : SObject
	{
		public string NamespacePrefix {set;get;}
		public string Name {set;get;}
		public double ApiVersion {set;get;}
		public string Status {set;get;}
		public bool IsValid {set;get;}
		public double BodyCrc {set;get;}
		public string Body {set;get;}
		public int LengthWithoutComments {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
