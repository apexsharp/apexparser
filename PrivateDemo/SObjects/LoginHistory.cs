namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class LoginHistory : SObject
	{
		public string UserId {set;get;}

		public DateTime LoginTime {set;get;}

		public string LoginType {set;get;}

		public string SourceIp {set;get;}

		public string LoginUrl {set;get;}

		public string AuthenticationServiceId {set;get;}

		public string LoginGeoId {set;get;}

		public LoginGeo LoginGeo {set;get;}

		public string TlsProtocol {set;get;}

		public string CipherSuite {set;get;}

		public string Browser {set;get;}

		public string Platform {set;get;}

		public string Status {set;get;}

		public string Application {set;get;}

		public string ClientVersion {set;get;}

		public string ApiType {set;get;}

		public string ApiVersion {set;get;}

		public string CountryIso {set;get;}
	}
}
