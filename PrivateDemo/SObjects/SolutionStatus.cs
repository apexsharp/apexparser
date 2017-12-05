namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SolutionStatus : SObject
	{
		public string MasterLabel {set;get;}

		public string ApiName {set;get;}

		public int SortOrder {set;get;}

		public bool IsDefault {set;get;}

		public bool IsReviewed {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
