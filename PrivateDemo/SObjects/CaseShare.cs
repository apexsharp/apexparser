namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CaseShare : SObject
	{
		public string CaseId {set;get;}

		public Case Case {set;get;}

		public string UserOrGroupId {set;get;}

		public Group UserOrGroup {set;get;}

		public string CaseAccessLevel {set;get;}

		public string RowCause {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public bool IsDeleted {set;get;}
	}
}
