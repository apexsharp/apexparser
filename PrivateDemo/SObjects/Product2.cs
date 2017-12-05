namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Product2 : SObject
	{
		public string Name {set;get;}

		public string ProductCode {set;get;}

		public string Description {set;get;}

		public bool IsActive {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string Family {set;get;}

		public string ExternalDataSourceId {set;get;}

		public string ExternalId {set;get;}

		public string DisplayUrl {set;get;}

		public string QuantityUnitOfMeasure {set;get;}

		public bool IsDeleted {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}
	}
}
