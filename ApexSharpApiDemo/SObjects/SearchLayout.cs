namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class SearchLayout : SObject
	{
		public string DurableId {set;get;}
		public string Label {set;get;}
		public string LayoutType {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public string EntityDefinitionId {set;get;}
		public string FieldsDisplayed {set;get;}
		public string ButtonsDisplayed {set;get;}
	}
}
