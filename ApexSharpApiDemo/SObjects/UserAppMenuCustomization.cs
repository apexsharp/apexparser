namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserAppMenuCustomization : SObject
	{
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string ApplicationId {set;get;}
		public ConnectedApplication Application {set;get;}
		public int SortOrder {set;get;}
	}
}
