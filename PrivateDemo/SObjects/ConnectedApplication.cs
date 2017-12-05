namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ConnectedApplication : SObject
	{
		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool OptionsAllowAdminApprovedUsersOnly {set;get;}

		public bool OptionsRefreshTokenValidityMetric {set;get;}

		public bool OptionsHasSessionLevelPolicy {set;get;}

		public bool OptionsIsInternal {set;get;}

		public string MobileSessionTimeout {set;get;}

		public string PinLength {set;get;}

		public string StartUrl {set;get;}

		public string MobileStartUrl {set;get;}

		public int RefreshTokenValidityPeriod {set;get;}
	}
}
