namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class VisualforceAccessMetrics : SObject
	{
		public DateTime MetricsDate {set;get;}
		public string ApexPageId {set;get;}
		public ApexPage ApexPage {set;get;}
		public DateTime SystemModstamp {set;get;}
		public int DailyPageViewCount {set;get;}
	}
}
