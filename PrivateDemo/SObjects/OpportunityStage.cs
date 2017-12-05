namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OpportunityStage : SObject
	{
		public string MasterLabel {set;get;}

		public string ApiName {set;get;}

		public bool IsActive {set;get;}

		public int SortOrder {set;get;}

		public bool IsClosed {set;get;}

		public bool IsWon {set;get;}

		public string ForecastCategory {set;get;}

		public string ForecastCategoryName {set;get;}

		public double DefaultProbability {set;get;}

		public string Description {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
