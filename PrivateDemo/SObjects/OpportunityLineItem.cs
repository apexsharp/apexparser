namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OpportunityLineItem : SObject
	{
		public string OpportunityId {set;get;}

		public Opportunity Opportunity {set;get;}

		public int SortOrder {set;get;}

		public string PricebookEntryId {set;get;}

		public PricebookEntry PricebookEntry {set;get;}

		public string Product2Id {set;get;}

		public Product2 Product2 {set;get;}

		public string ProductCode {set;get;}

		public string Name {set;get;}

		public double Quantity {set;get;}

		public double Discount {set;get;}

		public double Subtotal {set;get;}

		public double TotalPrice {set;get;}

		public double UnitPrice {set;get;}

		public double ListPrice {set;get;}

		public DateTime ServiceDate {set;get;}

		public string Description {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}
	}
}
