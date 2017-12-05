namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OwnedContentDocument : SObject
	{
		public bool IsDeleted {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string ContentDocumentId {set;get;}

		public ContentDocument ContentDocument {set;get;}

		public string Title {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string FileType {set;get;}

		public int ContentSize {set;get;}

		public string FileExtension {set;get;}

		public string ContentUrl {set;get;}

		public string ExternalDataSourceName {set;get;}

		public string ExternalDataSourceType {set;get;}
	}
}
