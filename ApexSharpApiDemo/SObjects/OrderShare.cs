namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OrderShare : SObject
	{
		public string OrderId {set;get;}
		public Order Order {set;get;}
		public string UserOrGroupId {set;get;}
		public Group UserOrGroup {set;get;}
		public string OrderAccessLevel {set;get;}
		public string RowCause {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public bool IsDeleted {set;get;}
	}
}
