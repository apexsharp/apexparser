namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OrderItem : SObject
	{
		public string Product2Id {set;get;}
		public Product2 Product2 {set;get;}
		public bool IsDeleted {set;get;}
		public string OrderId {set;get;}
		public Order Order {set;get;}
		public string PricebookEntryId {set;get;}
		public PricebookEntry PricebookEntry {set;get;}
		public string OriginalOrderItemId {set;get;}
		public OrderItem OriginalOrderItem {set;get;}
		public double AvailableQuantity {set;get;}
		public double Quantity {set;get;}
		public double UnitPrice {set;get;}
		public double ListPrice {set;get;}
		public double TotalPrice {set;get;}
		public DateTime ServiceDate {set;get;}
		public DateTime EndDate {set;get;}
		public string Description {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string OrderItemNumber {set;get;}
	}
}
