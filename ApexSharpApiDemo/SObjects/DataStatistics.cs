namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class DataStatistics : SObject
	{
		public string ExternalId {set;get;}
		public string StatType {set;get;}
		public string UserId {set;get;}
		public User User {set;get;}
		public string Type {set;get;}
		public int StatValue {set;get;}
	}
}
