namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OpportunityCompetitor : SObject
	{
		public string OpportunityId {set;get;}

		public Opportunity Opportunity {set;get;}

		public string CompetitorName {set;get;}

		public string Strengths {set;get;}

		public string Weaknesses {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}
	}
}
