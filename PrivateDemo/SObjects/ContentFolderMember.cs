namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentFolderMember : SObject
	{
		public string ParentContentFolderId {set;get;}

		public ContentFolder ParentContentFolder {set;get;}

		public string ChildRecordId {set;get;}

		public ContentDocument ChildRecord {set;get;}

		public bool IsDeleted {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}
	}
}
