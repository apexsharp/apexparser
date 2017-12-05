namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserAppMenuItem : SObject
	{
		public string AppMenuItemId {set;get;}

		public string ApplicationId {set;get;}

		public string Label {set;get;}

		public string Description {set;get;}

		public string Name {set;get;}

		public int UserSortOrder {set;get;}

		public int SortOrder {set;get;}

		public string Type {set;get;}

		public string LogoUrl {set;get;}

		public string IconUrl {set;get;}

		public string InfoUrl {set;get;}

		public string StartUrl {set;get;}

		public string MobileStartUrl {set;get;}

		public bool IsVisible {set;get;}

		public bool IsUsingAdminAuthorization {set;get;}
	}
}
