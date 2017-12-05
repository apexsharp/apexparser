namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Folder : SObject
	{
		public string Name {set;get;}

		public string DeveloperName {set;get;}

		public string AccessType {set;get;}

		public bool IsReadonly {set;get;}

		public string Type {set;get;}

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
