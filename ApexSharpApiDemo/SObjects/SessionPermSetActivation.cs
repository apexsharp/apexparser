namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SessionPermSetActivation : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string AuthSessionId {set;get;}
		public AuthSession AuthSession {set;get;}
		public string PermissionSetId {set;get;}
		public PermissionSet PermissionSet {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string Description {set;get;}
	}
}
