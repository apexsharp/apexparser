namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OpportunityHistory : SObject
	{
		public string OpportunityId {set;get;}
		public Opportunity Opportunity {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public string StageName {set;get;}
		public double Amount {set;get;}
		public double ExpectedRevenue {set;get;}
		public DateTime CloseDate {set;get;}
		public double Probability {set;get;}
		public string ForecastCategory {set;get;}
		public DateTime SystemModstamp {set;get;}
		public bool IsDeleted {set;get;}
	}
}
