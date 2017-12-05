namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OrgLifecycleNotification : SObject
	{
		public string ReplayId {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public string LifecycleRequestType {set;get;}

		public string LifecycleRequestId {set;get;}

		public string OrgId {set;get;}

		public string Status {set;get;}

		public string StatusCode {set;get;}
	}
}
