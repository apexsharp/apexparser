namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Solution : SObject
	{
		public bool IsDeleted {set;get;}
		public string SolutionNumber {set;get;}
		public string SolutionName {set;get;}
		public bool IsPublished {set;get;}
		public bool IsPublishedInPublicKb {set;get;}
		public string Status {set;get;}
		public bool IsReviewed {set;get;}
		public string SolutionNote {set;get;}
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public int TimesUsed {set;get;}
		public DateTime LastViewedDate {set;get;}
		public DateTime LastReferencedDate {set;get;}
		public bool IsHtml {set;get;}
	}
}
