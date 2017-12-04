namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Event : SObject
	{
		public string WhoId {set;get;}
		public Contact Who {set;get;}
		public string WhatId {set;get;}
		public Account What {set;get;}
		public string Subject {set;get;}
		public string Location {set;get;}
		public bool IsAllDayEvent {set;get;}
		public DateTime ActivityDateTime {set;get;}
		public DateTime ActivityDate {set;get;}
		public int DurationInMinutes {set;get;}
		public DateTime StartDateTime {set;get;}
		public DateTime EndDateTime {set;get;}
		public string Description {set;get;}
		public string AccountId {set;get;}
		public Account Account {set;get;}
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public string Type {set;get;}
		public bool IsPrivate {set;get;}
		public string ShowAs {set;get;}
		public bool IsDeleted {set;get;}
		public bool IsChild {set;get;}
		public bool IsGroupEvent {set;get;}
		public string GroupEventType {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public bool IsArchived {set;get;}
		public string RecurrenceActivityId {set;get;}
		public bool IsRecurrence {set;get;}
		public DateTime RecurrenceStartDateTime {set;get;}
		public DateTime RecurrenceEndDateOnly {set;get;}
		public string RecurrenceTimeZoneSidKey {set;get;}
		public string RecurrenceType {set;get;}
		public int RecurrenceInterval {set;get;}
		public int RecurrenceDayOfWeekMask {set;get;}
		public int RecurrenceDayOfMonth {set;get;}
		public string RecurrenceInstance {set;get;}
		public string RecurrenceMonthOfYear {set;get;}
		public DateTime ReminderDateTime {set;get;}
		public bool IsReminderSet {set;get;}
		public string EventSubtype {set;get;}
	}
}
