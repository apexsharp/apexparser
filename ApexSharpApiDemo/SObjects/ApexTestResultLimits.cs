namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexTestResultLimits : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string ApexTestResultId {set;get;}
		public ApexTestResult ApexTestResult {set;get;}
		public int Soql {set;get;}
		public int QueryRows {set;get;}
		public int Sosl {set;get;}
		public int Dml {set;get;}
		public int DmlRows {set;get;}
		public int Cpu {set;get;}
		public int Callouts {set;get;}
		public int Email {set;get;}
		public int AsyncCalls {set;get;}
		public int MobilePush {set;get;}
		public string LimitContext {set;get;}
		public string LimitExceptions {set;get;}
	}
}
