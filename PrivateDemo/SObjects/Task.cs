namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Task : SObject
	{
		public string WhoId {set;get;}

		public Contact Who {set;get;}

		public string WhatId {set;get;}

		public Account What {set;get;}

		public string Subject {set;get;}

		public DateTime ActivityDate {set;get;}

		public string Status {set;get;}

		public string Priority {set;get;}

		public bool IsHighPriority {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string Description {set;get;}

		public string Type {set;get;}

		public bool IsDeleted {set;get;}

		public string AccountId {set;get;}

		public Account Account {set;get;}

		public bool IsClosed {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public bool IsArchived {set;get;}

		public int CallDurationInSeconds {set;get;}

		public string CallType {set;get;}

		public string CallDisposition {set;get;}

		public string CallObject {set;get;}

		public DateTime ReminderDateTime {set;get;}

		public bool IsReminderSet {set;get;}

		public string RecurrenceActivityId {set;get;}

		public bool IsRecurrence {set;get;}

		public DateTime RecurrenceStartDateOnly {set;get;}

		public DateTime RecurrenceEndDateOnly {set;get;}

		public string RecurrenceTimeZoneSidKey {set;get;}

		public string RecurrenceType {set;get;}

		public int RecurrenceInterval {set;get;}

		public int RecurrenceDayOfWeekMask {set;get;}

		public int RecurrenceDayOfMonth {set;get;}

		public string RecurrenceInstance {set;get;}

		public string RecurrenceMonthOfYear {set;get;}

		public string RecurrenceRegeneratedType {set;get;}

		public string TaskSubtype {set;get;}
	}
}
