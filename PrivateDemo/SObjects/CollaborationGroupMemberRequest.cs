namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CollaborationGroupMemberRequest : SObject
	{
		public string CollaborationGroupId {set;get;}

		public CollaborationGroup CollaborationGroup {set;get;}

		public string RequesterId {set;get;}

		public User Requester {set;get;}

		public string ResponseMessage {set;get;}

		public string Status {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
