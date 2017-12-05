namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CaseSolution : SObject
	{
		public string CaseId {set;get;}

		public Case Case {set;get;}

		public string SolutionId {set;get;}

		public Solution Solution {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime CreatedDate {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsDeleted {set;get;}
	}
}
