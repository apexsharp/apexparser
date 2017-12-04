namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ExternalDataUserAuth : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string ExternalDataSourceId {set;get;}
		public ExternalDataSource ExternalDataSource {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string Protocol {set;get;}
		public string Username {set;get;}
		public string Password {set;get;}
		public string AuthProviderId {set;get;}
		public AuthProvider AuthProvider {set;get;}
	}
}
