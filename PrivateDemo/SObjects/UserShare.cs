namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserShare : SObject
	{
		public string UserId {set;get;}

		public User User {set;get;}

		public string UserOrGroupId {set;get;}

		public Group UserOrGroup {set;get;}

		public string UserAccessLevel {set;get;}

		public string RowCause {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public bool IsActive {set;get;}
	}
}
