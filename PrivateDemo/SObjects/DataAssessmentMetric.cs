namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DataAssessmentMetric : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public int NumTotal {set;get;}

		public int NumProcessed {set;get;}

		public int NumMatched {set;get;}

		public int NumMatchedDifferent {set;get;}

		public int NumUnmatched {set;get;}

		public int NumDuplicates {set;get;}
	}
}
