namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AssistantRecommendation : SObject
	{
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public bool IsDeleted {set;get;}
		public string Name {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string RecordId {set;get;}
		public Account Record {set;get;}
		public double BaseScore {set;get;}
		public string RecommendationType {set;get;}
		public DateTime StartTime {set;get;}
		public DateTime EndTime {set;get;}
		public string Reason {set;get;}
		public string ActionPrepopulationValues {set;get;}
	}
}
