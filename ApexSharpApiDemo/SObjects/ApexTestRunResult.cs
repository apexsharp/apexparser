namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexTestRunResult : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string AsyncApexJobId {set;get;}
		public AsyncApexJob AsyncApexJob {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string JobName {set;get;}
		public bool IsAllTests {set;get;}
		public string Source {set;get;}
		public DateTime StartTime {set;get;}
		public DateTime EndTime {set;get;}
		public int TestTime {set;get;}
		public string Status {set;get;}
		public int ClassesEnqueued {set;get;}
		public int ClassesCompleted {set;get;}
		public int MethodsEnqueued {set;get;}
		public int MethodsCompleted {set;get;}
		public int MethodsFailed {set;get;}
	}
}
