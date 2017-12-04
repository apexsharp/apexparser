namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserListView : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string ListViewId {set;get;}
		public ListView ListView {set;get;}
		public string SobjectType {set;get;}
		public string LastViewedChart {set;get;}
	}
}
