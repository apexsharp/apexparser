namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Product2History : SObject
	{
		public bool IsDeleted {set;get;}
		public string Product2Id {set;get;}
		public Product2 Product2 {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public string Field {set;get;}
		public object OldValue {set;get;}
		public object NewValue {set;get;}
	}
}
