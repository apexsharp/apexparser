namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserLogin : SObject
	{
		public string UserId {set;get;}

		public bool IsFrozen {set;get;}

		public bool IsPasswordLocked {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}
	}
}
