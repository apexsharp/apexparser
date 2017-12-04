namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class IdpEventLog : SObject
	{
		public string InitiatedBy {set;get;}
		public DateTime Timestamp {set;get;}
		public string ErrorCode {set;get;}
		public string SamlEntityUrl {set;get;}
		public string UserId {set;get;}
		public string AuthSessionId {set;get;}
		public string SsoType {set;get;}
		public string AppId {set;get;}
		public string IdentityUsed {set;get;}
		public bool OptionsHasLogoutUrl {set;get;}
	}
}
