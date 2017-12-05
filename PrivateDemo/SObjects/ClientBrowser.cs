namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ClientBrowser : SObject
	{
		public string UsersId {set;get;}

		public User Users {set;get;}

		public string FullUserAgent {set;get;}

		public string ProxyInfo {set;get;}

		public DateTime LastUpdate {set;get;}

		public DateTime CreatedDate {set;get;}
	}
}
