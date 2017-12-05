namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Holiday : SObject
	{
		public string Name {set;get;}

		public string Description {set;get;}

		public bool IsAllDay {set;get;}

		public DateTime ActivityDate {set;get;}

		public int StartTimeInMinutes {set;get;}

		public int EndTimeInMinutes {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsRecurrence {set;get;}

		public DateTime RecurrenceStartDate {set;get;}

		public DateTime RecurrenceEndDateOnly {set;get;}

		public string RecurrenceType {set;get;}

		public int RecurrenceInterval {set;get;}

		public int RecurrenceDayOfWeekMask {set;get;}

		public int RecurrenceDayOfMonth {set;get;}

		public string RecurrenceInstance {set;get;}

		public string RecurrenceMonthOfYear {set;get;}
	}
}
