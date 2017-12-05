namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class UserProvAccount : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string SalesforceUserId {set;get;}

		public User SalesforceUser {set;get;}

		public string ConnectedAppId {set;get;}

		public ConnectedApplication ConnectedApp {set;get;}

		public string ExternalUserId {set;get;}

		public string ExternalUsername {set;get;}

		public string ExternalEmail {set;get;}

		public string ExternalFirstName {set;get;}

		public string ExternalLastName {set;get;}

		public string LinkState {set;get;}

		public string Status {set;get;}

		public DateTime DeletedDate {set;get;}

		public bool IsKnownLink {set;get;}
	}
}
