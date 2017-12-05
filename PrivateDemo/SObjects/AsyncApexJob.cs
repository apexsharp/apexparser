namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class AsyncApexJob : SObject
	{
		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public string JobType {set;get;}

		public string ApexClassId {set;get;}

		public ApexClass ApexClass {set;get;}

		public string Status {set;get;}

		public int JobItemsProcessed {set;get;}

		public int TotalJobItems {set;get;}

		public int NumberOfErrors {set;get;}

		public DateTime CompletedDate {set;get;}

		public string MethodName {set;get;}

		public string ExtendedStatus {set;get;}

		public string ParentJobId {set;get;}

		public string LastProcessed {set;get;}

		public int LastProcessedOffset {set;get;}
	}
}
