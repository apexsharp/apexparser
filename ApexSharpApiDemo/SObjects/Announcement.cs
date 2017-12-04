namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Announcement : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string FeedItemId {set;get;}
		public FeedItem FeedItem {set;get;}
		public DateTime ExpirationDate {set;get;}
		public bool SendEmails {set;get;}
		public bool IsArchived {set;get;}
		public string ParentId {set;get;}
		public CollaborationGroup Parent {set;get;}
	}
}
