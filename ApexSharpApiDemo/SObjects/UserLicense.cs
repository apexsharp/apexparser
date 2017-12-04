namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserLicense : SObject
	{
		public string LicenseDefinitionKey {set;get;}
		public int TotalLicenses {set;get;}
		public string Status {set;get;}
		public int UsedLicenses {set;get;}
		public string Name {set;get;}
		public string MasterLabel {set;get;}
		public int MonthlyLoginsUsed {set;get;}
		public int MonthlyLoginsEntitlement {set;get;}
		public DateTime CreatedDate {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
