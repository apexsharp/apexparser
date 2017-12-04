namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PackageLicense : SObject
	{
		public string Status {set;get;}
		public bool IsProvisioned {set;get;}
		public int AllowedLicenses {set;get;}
		public int UsedLicenses {set;get;}
		public DateTime ExpirationDate {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string NamespacePrefix {set;get;}
	}
}
