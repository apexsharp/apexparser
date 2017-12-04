namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CombinedAttachment : SObject
	{
		public bool IsDeleted {set;get;}
		public string ParentId {set;get;}
		public Account Parent {set;get;}
		public string RecordType {set;get;}
		public string Title {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public string FileType {set;get;}
		public int ContentSize {set;get;}
		public string FileExtension {set;get;}
		public string ContentUrl {set;get;}
		public string ExternalDataSourceName {set;get;}
		public string ExternalDataSourceType {set;get;}
		public string SharingOption {set;get;}
	}
}
