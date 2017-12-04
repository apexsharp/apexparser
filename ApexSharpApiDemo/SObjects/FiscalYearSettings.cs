namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class FiscalYearSettings : SObject
	{
		public string PeriodId {set;get;}
		public DateTime StartDate {set;get;}
		public DateTime EndDate {set;get;}
		public string Name {set;get;}
		public bool IsStandardYear {set;get;}
		public string YearType {set;get;}
		public string QuarterLabelScheme {set;get;}
		public string PeriodLabelScheme {set;get;}
		public string WeekLabelScheme {set;get;}
		public string QuarterPrefix {set;get;}
		public string PeriodPrefix {set;get;}
		public int WeekStartDay {set;get;}
		public string Description {set;get;}
		public DateTime SystemModstamp {set;get;}
	}
}
