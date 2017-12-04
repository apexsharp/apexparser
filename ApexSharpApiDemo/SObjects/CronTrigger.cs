namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CronTrigger : SObject
	{
		public string CronJobDetailId {set;get;}
		public CronJobDetail CronJobDetail {set;get;}
		public DateTime NextFireTime {set;get;}
		public DateTime PreviousFireTime {set;get;}
		public string State {set;get;}
		public DateTime StartTime {set;get;}
		public DateTime EndTime {set;get;}
		public string CronExpression {set;get;}
		public string TimeZoneSidKey {set;get;}
		public string OwnerId {set;get;}
		public string LastModifiedById {set;get;}
		public User LastModifiedBy {set;get;}
		public string CreatedById {set;get;}
		public User CreatedBy {set;get;}
		public DateTime CreatedDate {set;get;}
		public int TimesTriggered {set;get;}
	}
}
