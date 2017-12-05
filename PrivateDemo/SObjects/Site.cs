namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Site : SObject
	{
		public string Name {set;get;}

		public string Subdomain {set;get;}

		public string UrlPathPrefix {set;get;}

		public string GuestUserId {set;get;}

		public User GuestUser {set;get;}

		public string Status {set;get;}

		public string AdminId {set;get;}

		public User Admin {set;get;}

		public bool OptionsEnableFeeds {set;get;}

		public bool OptionsRequireHttps {set;get;}

		public bool OptionsAllowHomePage {set;get;}

		public bool OptionsAllowStandardIdeasPages {set;get;}

		public bool OptionsAllowStandardSearch {set;get;}

		public bool OptionsAllowStandardLookups {set;get;}

		public bool OptionsAllowStandardAnswersPages {set;get;}

		public bool OptionsAllowGuestSupportApi {set;get;}

		public bool OptionsAllowStandardPortalPages {set;get;}

		public string Description {set;get;}

		public string MasterLabel {set;get;}

		public string AnalyticsTrackingCode {set;get;}

		public string SiteType {set;get;}

		public string ClickjackProtectionLevel {set;get;}

		public int DailyBandwidthLimit {set;get;}

		public int DailyBandwidthUsed {set;get;}

		public int DailyRequestTimeLimit {set;get;}

		public int DailyRequestTimeUsed {set;get;}

		public int MonthlyPageViewsEntitlement {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
