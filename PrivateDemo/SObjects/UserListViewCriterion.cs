namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserListViewCriterion : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string UserListViewId {set;get;}

		public UserListView UserListView {set;get;}

		public int SortOrder {set;get;}

		public string ColumnName {set;get;}

		public string Operation {set;get;}

		public string Value {set;get;}
	}
}
