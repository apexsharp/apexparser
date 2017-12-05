namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CustomObjectUserLicenseMetrics : SObject
	{
		public DateTime MetricsDate {set;get;}

		public string UserLicenseId {set;get;}

		public UserLicense UserLicense {set;get;}

		public string CustomObjectId {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string CustomObjectType {set;get;}

		public string CustomObjectName {set;get;}

		public int ObjectCount {set;get;}
	}
}
