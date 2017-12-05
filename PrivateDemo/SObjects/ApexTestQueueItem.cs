namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexTestQueueItem : SObject
	{
		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ApexClassId {set;get;}

		public ApexClass ApexClass {set;get;}

		public string Status {set;get;}

		public string ExtendedStatus {set;get;}

		public string ParentJobId {set;get;}

		public string TestRunResultId {set;get;}
	}
}
