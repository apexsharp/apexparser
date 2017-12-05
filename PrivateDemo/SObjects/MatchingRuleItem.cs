namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class MatchingRuleItem : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string MatchingRuleId {set;get;}

		public MatchingRule MatchingRule {set;get;}

		public int SortOrder {set;get;}

		public string Field {set;get;}

		public string MatchingMethod {set;get;}

		public string BlankValueBehavior {set;get;}
	}
}
