namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AppMenuItem : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public int SortOrder {set;get;}
		public string Name {set;get;}
		public string NamespacePrefix {set;get;}
		public string Label {set;get;}
		public string Description {set;get;}
		public string StartUrl {set;get;}
		public string MobileStartUrl {set;get;}
		public string LogoUrl {set;get;}
		public string IconUrl {set;get;}
		public string InfoUrl {set;get;}
		public bool IsUsingAdminAuthorization {set;get;}
		public string MobilePlatform {set;get;}
		public string MobileMinOsVer {set;get;}
		public string MobileDeviceType {set;get;}
		public bool IsRegisteredDeviceOnly {set;get;}
		public string MobileAppVer {set;get;}
		public DateTime MobileAppInstalledDate {set;get;}
		public string MobileAppInstalledVersion {set;get;}
		public string MobileAppBinaryId {set;get;}
		public string MobileAppInstallUrl {set;get;}
		public bool CanvasEnabled {set;get;}
		public string CanvasReferenceId {set;get;}
		public string CanvasUrl {set;get;}
		public string CanvasAccessMethod {set;get;}
		public string CanvasSelectedLocations {set;get;}
		public string CanvasOptions {set;get;}
		public string Type {set;get;}
		public string ApplicationId {set;get;}
		public int UserSortOrder {set;get;}
		public bool IsVisible {set;get;}
		public bool IsAccessible {set;get;}
	}
}
