namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class TenantUsageEntitlement : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ResourceGroupKey {set;get;}

		public string Setting {set;get;}

		public DateTime StartDate {set;get;}

		public DateTime EndDate {set;get;}

		public double CurrentAmountAllowed {set;get;}

		public string Frequency {set;get;}

		public bool IsPersistentResource {set;get;}

		public bool HasRollover {set;get;}

		public double OverageGrace {set;get;}

		public double AmountUsed {set;get;}

		public DateTime UsageDate {set;get;}

		public string MasterLabel {set;get;}
	}
}
