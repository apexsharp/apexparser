namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Period : SObject
	{
		public string FiscalYearSettingsId {set;get;}

		public FiscalYearSettings FiscalYearSettings {set;get;}

		public string Type {set;get;}

		public DateTime StartDate {set;get;}

		public DateTime EndDate {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsForecastPeriod {set;get;}

		public string QuarterLabel {set;get;}

		public string PeriodLabel {set;get;}

		public int Number {set;get;}

		public string FullyQualifiedLabel {set;get;}
	}
}
