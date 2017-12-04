namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CollaborationInvitation : SObject
	{
		public string ParentId {set;get;}
		public string SharedEntityId {set;get;}
		public string InviterId {set;get;}
		public string InvitedUserEmail {set;get;}
		public string InvitedUserEmailNormalized {set;get;}
		public string Status {set;get;}
		public string OptionalMessage {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
