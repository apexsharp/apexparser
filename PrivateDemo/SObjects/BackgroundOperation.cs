namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class BackgroundOperation : SObject
	{
		public bool IsDeleted {set;get;}

		public string Name {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime SubmittedAt {set;get;}

		public string Status {set;get;}

		public string ExecutionGroup {set;get;}

		public string SequenceGroup {set;get;}

		public int SequenceNumber {set;get;}

		public string GroupLeaderId {set;get;}

		public BackgroundOperation GroupLeader {set;get;}

		public DateTime StartedAt {set;get;}

		public DateTime FinishedAt {set;get;}

		public string WorkerUri {set;get;}

		public int Timeout {set;get;}

		public DateTime ExpiresAt {set;get;}

		public int NumFollowers {set;get;}

		public DateTime ProcessAfter {set;get;}

		public string ParentKey {set;get;}

		public int RetryLimit {set;get;}

		public int RetryCount {set;get;}

		public int RetryBackoff {set;get;}

		public string Error {set;get;}
	}
}
