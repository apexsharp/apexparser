namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class TestSuiteMembership : SObject
	{
		public bool IsDeleted {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ApexTestSuiteId {set;get;}

		public ApexTestSuite ApexTestSuite {set;get;}

		public string ApexClassId {set;get;}

		public ApexClass ApexClass {set;get;}
	}
}
