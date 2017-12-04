namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class MatchingRule : SObject
	{
		public bool IsDeleted {set;get;}
		public string SobjectType {set;get;}
		public string DeveloperName {set;get;}
		public string Language {set;get;}
		public string MasterLabel {set;get;}
		public string NamespacePrefix {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string MatchEngine {set;get;}
		public string BooleanFilter {set;get;}
		public string Description {set;get;}
		public string RuleStatus {set;get;}
		public string SobjectSubtype {set;get;}
	}
}
