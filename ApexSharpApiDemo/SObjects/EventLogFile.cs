namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class EventLogFile : SObject
	{
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public string EventType {set;get;}
		public DateTime LogDate {set;get;}
		public double LogFileLength {set;get;}
		public string LogFileContentType {set;get;}
		public double ApiVersion {set;get;}
		public string LogFileFieldNames {set;get;}
		public string LogFileFieldTypes {set;get;}
		public string LogFile {set;get;}
	}
}
