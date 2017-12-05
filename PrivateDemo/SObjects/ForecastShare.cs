namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ForecastShare : SObject
	{
		public string UserRoleId {set;get;}

		public string UserOrGroupId {set;get;}

		public string AccessLevel {set;get;}

		public bool CanSubmit {set;get;}

		public string RowCause {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}
	}
}
