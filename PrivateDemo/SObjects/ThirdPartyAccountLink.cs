namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ThirdPartyAccountLink : SObject
	{
		public string ThirdPartyAccountLinkKey {set;get;}

		public string UserId {set;get;}

		public User User {set;get;}

		public string SsoProviderId {set;get;}

		public AuthProvider SsoProvider {set;get;}

		public string Handle {set;get;}

		public string RemoteIdentifier {set;get;}

		public string Provider {set;get;}

		public string SsoProviderName {set;get;}

		public bool IsNotSsoUsable {set;get;}
	}
}
