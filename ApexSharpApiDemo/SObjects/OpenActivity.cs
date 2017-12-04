namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class OpenActivity : SObject
	{
		public string AccountId {set;get;}
		public Account Account {set;get;}
		public string WhoId {set;get;}
		public Contact Who {set;get;}
		public string WhatId {set;get;}
		public Account What {set;get;}
		public string Subject {set;get;}
		public bool IsTask {set;get;}
		public DateTime ActivityDate {set;get;}
		public string OwnerId {set;get;}
		public User Owner {set;get;}
		public string Status {set;get;}
		public string Priority {set;get;}
		public bool IsHighPriority {set;get;}
		public string ActivityType {set;get;}
		public bool IsClosed {set;get;}
		public bool IsAllDayEvent {set;get;}
		public bool IsVisibleInSelfService {set;get;}
		public int DurationInMinutes {set;get;}
		public string Location {set;get;}
		public string Description {set;get;}
		public bool IsDeleted {set;get;}
		public DateTime CreatedDate {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime LastModifiedDate {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public DateTime SystemModstamp {set;get;}
		public int CallDurationInSeconds {set;get;}
		public string CallType {set;get;}
		public string CallDisposition {set;get;}
		public string CallObject {set;get;}
		public DateTime ReminderDateTime {set;get;}
		public bool IsReminderSet {set;get;}
		public DateTime EndDateTime {set;get;}
		public DateTime StartDateTime {set;get;}
		public string ActivitySubtype {set;get;}
		public string AlternateDetailId {set;get;}
		public EmailMessage AlternateDetail {set;get;}
	}
}
