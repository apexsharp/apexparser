namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DataAssessmentFieldMetric : SObject
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
		public string DataAssessmentMetricId {set;get;}
		public DataAssessmentMetric DataAssessmentMetric {set;get;}
		public string FieldName {set;get;}
		public int NumMatchedInSync {set;get;}
		public int NumMatchedDifferent {set;get;}
		public int NumMatchedBlanks {set;get;}
		public int NumUnmatchedBlanks {set;get;}
	}
}
