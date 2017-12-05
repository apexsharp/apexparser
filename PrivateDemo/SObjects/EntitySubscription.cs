namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EntitySubscription : SObject
	{
		public string ParentId {set;get;}

		public Account Parent {set;get;}

		public string SubscriberId {set;get;}

		public User Subscriber {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public bool IsDeleted {set;get;}
	}
}
