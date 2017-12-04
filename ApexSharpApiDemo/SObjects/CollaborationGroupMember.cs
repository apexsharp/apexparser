namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CollaborationGroupMember : SObject
	{
		public string CollaborationGroupId {set;get;}
		public CollaborationGroup CollaborationGroup {set;get;}
		public string MemberId {set;get;}
		public User Member {set;get;}
		public string CollaborationRole {set;get;}
		public string NotificationFrequency {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime LastFeedAccessDate {set;get;}
	}
}
