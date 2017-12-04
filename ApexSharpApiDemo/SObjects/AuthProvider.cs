namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AuthProvider : SObject
	{
		public DateTime CreatedDate {set;get;}
		public string ProviderType {set;get;}
		public string FriendlyName {set;get;}
		public string DeveloperName {set;get;}
		public string RegistrationHandlerId {set;get;}
		public string ExecutionUserId {set;get;}
		public string ConsumerKey {set;get;}
		public string ConsumerSecret {set;get;}
		public string ErrorUrl {set;get;}
		public string AuthorizeUrl {set;get;}
		public string TokenUrl {set;get;}
		public string UserInfoUrl {set;get;}
		public string DefaultScopes {set;get;}
		public string IdTokenIssuer {set;get;}
		public bool OptionsSendAccessTokenInHeader {set;get;}
		public bool OptionsSendClientCredentialsInHeader {set;get;}
		public bool OptionsIncludeOrgIdInId {set;get;}
		public string IconUrl {set;get;}
		public string LogoutUrl {set;get;}
		public string PluginId {set;get;}
		public string CustomMetadataTypeRecord {set;get;}
	}
}
