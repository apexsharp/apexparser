namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContentDocument : SObject
	{
		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public bool IsArchived {set;get;}

		public string ArchivedById {set;get;}

		public DateTime ArchivedDate {set;get;}

		public bool IsDeleted {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string Title {set;get;}

		public string PublishStatus {set;get;}

		public string LatestPublishedVersionId {set;get;}

		public ContentVersion LatestPublishedVersion {set;get;}

		public string ParentId {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string Description {set;get;}

		public int ContentSize {set;get;}

		public string FileType {set;get;}

		public string FileExtension {set;get;}

		public string SharingOption {set;get;}

		public DateTime ContentModifiedDate {set;get;}

		public string ContentAssetId {set;get;}

		public ContentAsset ContentAsset {set;get;}
	}
}
