namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AccountShare : SObject
	{
		public string AccountId {set;get;}

		public Account Account {set;get;}

		public string UserOrGroupId {set;get;}

		public Group UserOrGroup {set;get;}

		public string AccountAccessLevel {set;get;}

		public string OpportunityAccessLevel {set;get;}

		public string CaseAccessLevel {set;get;}

		public string ContactAccessLevel {set;get;}

		public string RowCause {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public bool IsDeleted {set;get;}
	}
}
