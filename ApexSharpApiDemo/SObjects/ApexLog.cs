namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ApexLog : SObject
	{
		public string LogUserId {set;get;}
		public User LogUser {set;get;}
		public int LogLength {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string Request {set;get;}
		public string Operation {set;get;}
		public string Application {set;get;}
		public string Status {set;get;}
		public int DurationMilliseconds {set;get;}
		public DateTime SystemModstamp {set;get;}
		public DateTime StartTime {set;get;}
		public string Location {set;get;}
	}
}
