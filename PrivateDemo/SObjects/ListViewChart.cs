namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ListViewChart : SObject
	{
		public bool IsDeleted {set;get;}

		public string SobjectType {set;get;}

		public string DeveloperName {set;get;}

		public string Language {set;get;}

		public string MasterLabel {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string ChartType {set;get;}

		public string GroupingField {set;get;}

		public string AggregateField {set;get;}

		public string AggregateType {set;get;}
	}
}
