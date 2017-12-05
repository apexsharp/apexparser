namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DatacloudPurchaseUsage : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string UserId {set;get;}

		public User User {set;get;}

		public string UserType {set;get;}

		public string PurchaseType {set;get;}

		public string DatacloudEntityType {set;get;}

		public double Usage {set;get;}

		public string Description {set;get;}
	}
}
