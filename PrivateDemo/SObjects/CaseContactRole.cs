namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CaseContactRole : SObject
	{
		public string CasesId {set;get;}

		public Case Cases {set;get;}

		public string ContactId {set;get;}

		public Contact Contact {set;get;}

		public string Role {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}
	}
}
