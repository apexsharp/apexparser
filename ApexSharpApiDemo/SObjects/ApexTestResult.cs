namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexTestResult : SObject
	{
		public DateTime SystemModstamp {set;get;}
		public DateTime TestTimestamp {set;get;}
		public string Outcome {set;get;}
		public string ApexClassId {set;get;}
		public ApexClass ApexClass {set;get;}
		public string MethodName {set;get;}
		public string Message {set;get;}
		public string StackTrace {set;get;}
		public string AsyncApexJobId {set;get;}
		public AsyncApexJob AsyncApexJob {set;get;}
		public string QueueItemId {set;get;}
		public ApexTestQueueItem QueueItem {set;get;}
		public string ApexLogId {set;get;}
		public ApexLog ApexLog {set;get;}
		public string ApexTestRunResultId {set;get;}
		public ApexTestRunResult ApexTestRunResult {set;get;}
		public int RunTime {set;get;}
	}
}
