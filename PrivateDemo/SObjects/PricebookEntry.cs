namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class PricebookEntry : SObject
	{
		public string Name {set;get;}

		public string Pricebook2Id {set;get;}

		public Pricebook2 Pricebook2 {set;get;}

		public string Product2Id {set;get;}

		public Product2 Product2 {set;get;}

		public double UnitPrice {set;get;}

		public bool IsActive {set;get;}

		public bool UseStandardPrice {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ProductCode {set;get;}

		public bool IsDeleted {set;get;}
	}
}
