namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OpportunityShare : SObject
	{
		public string OpportunityId {set;get;}
		public Opportunity Opportunity {set;get;}
		public string UserOrGroupId {set;get;}
		public Group UserOrGroup {set;get;}
		public string OpportunityAccessLevel {set;get;}
		public string RowCause {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public bool IsDeleted {set;get;}
	}
}
