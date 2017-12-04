namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ListViewChartInstance : SObject
	{
		public string ExternalId {set;get;}
		public string ListViewChartId {set;get;}
		public ListViewChart ListViewChart {set;get;}
		public string Label {set;get;}
		public string DeveloperName {set;get;}
		public string SourceEntity {set;get;}
		public string ListViewContextId {set;get;}
		public ListView ListViewContext {set;get;}
		public string ChartType {set;get;}
		public bool IsLastViewed {set;get;}
		public string DataQuery {set;get;}
		public bool IsEditable {set;get;}
		public bool IsDeletable {set;get;}
		public string GroupingField {set;get;}
		public string AggregateField {set;get;}
		public string AggregateType {set;get;}
	}
}
