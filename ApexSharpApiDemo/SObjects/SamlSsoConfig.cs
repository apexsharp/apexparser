namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SamlSsoConfig : SObject
	{
		public bool IsDeleted {set;get;}
		public string DeveloperName {set;get;}
		public string Language {set;get;}
		public string MasterLabel {set;get;}
		public string NamespacePrefix {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string Version {set;get;}
		public string Issuer {set;get;}
		public bool OptionsSpInitBinding {set;get;}
		public bool OptionsUserProvisioning {set;get;}
		public string AttributeFormat {set;get;}
		public string AttributeName {set;get;}
		public string Audience {set;get;}
		public string IdentityMapping {set;get;}
		public string IdentityLocation {set;get;}
		public string SamlJitHandlerId {set;get;}
		public ApexClass SamlJitHandler {set;get;}
		public string ExecutionUserId {set;get;}
		public User ExecutionUser {set;get;}
		public string LoginUrl {set;get;}
		public string LogoutUrl {set;get;}
		public string ErrorUrl {set;get;}
		public string ValidationCert {set;get;}
		public string RequestSignatureMethod {set;get;}
	}
}
