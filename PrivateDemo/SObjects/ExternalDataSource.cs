namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ExternalDataSource : SObject
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

		public string Type {set;get;}

		public string Endpoint {set;get;}

		public string Repository {set;get;}

		public bool IsWritable {set;get;}

		public string PrincipalType {set;get;}

		public string Protocol {set;get;}

		public string AuthProviderId {set;get;}

		public AuthProvider AuthProvider {set;get;}

		public string LargeIconId {set;get;}

		public StaticResource LargeIcon {set;get;}

		public string SmallIconId {set;get;}

		public StaticResource SmallIcon {set;get;}

		public string CustomConfiguration {set;get;}
	}
}
