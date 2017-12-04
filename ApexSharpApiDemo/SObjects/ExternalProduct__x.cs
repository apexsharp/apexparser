namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ExternalProduct__x : SObject
	{
		public string ExternalId {set;get;}
		public string DisplayUrl {set;get;}
		public double CategoryID__c {set;get;}
		public bool Discontinued__c {set;get;}
		public double ProductID__c {set;get;}
		public string ProductName__c {set;get;}
		public string QuantityPerUnit__c {set;get;}
		public double ReorderLevel__c {set;get;}
		public double SupplierID__c {set;get;}
		public double UnitPrice__c {set;get;}
		public double UnitsInStock__c {set;get;}
		public double UnitsOnOrder__c {set;get;}
	}
}
