namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OauthToken : SObject
	{
		public string AccessToken {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string RequestToken {set;get;}
		public DateTime CreatedDate {set;get;}
		public string AppName {set;get;}
		public DateTime LastUsedDate {set;get;}
		public int UseCount {set;get;}
		public string DeleteToken {set;get;}
		public string AppMenuItemId {set;get;}
		public AppMenuItem AppMenuItem {set;get;}
	}
}
