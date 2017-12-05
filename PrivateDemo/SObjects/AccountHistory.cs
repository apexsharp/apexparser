namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AccountHistory : SObject
	{
		public bool IsDeleted {set;get;}

		public string AccountId {set;get;}

		public Account Account {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string Field {set;get;}

		public object OldValue {set;get;}

		public object NewValue {set;get;}
	}
}
