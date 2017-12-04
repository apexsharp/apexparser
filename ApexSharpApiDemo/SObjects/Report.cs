namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Report : SObject
	{
		public string OwnerId {set;get;}
		public Organization Owner {set;get;}
		public string FolderName {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public bool IsDeleted {set;get;}
		public string Name {set;get;}
		public string Description {set;get;}
		public string DeveloperName {set;get;}
		public string NamespacePrefix {set;get;}
		public DateTime LastRunDate {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string Format {set;get;}
		public DateTime LastViewedDate {set;get;}
		public DateTime LastReferencedDate {set;get;}
	}
}
