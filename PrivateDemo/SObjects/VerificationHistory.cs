namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class VerificationHistory : SObject
	{
		public int EventGroup {set;get;}

		public DateTime VerificationTime {set;get;}

		public string VerificationMethod {set;get;}

		public string UserId {set;get;}

		public User User {set;get;}

		public string Activity {set;get;}

		public string Status {set;get;}

		public string LoginHistoryId {set;get;}

		public LoginHistory LoginHistory {set;get;}

		public string SourceIp {set;get;}

		public string LoginGeoId {set;get;}

		public LoginGeo LoginGeo {set;get;}

		public string Remarks {set;get;}

		public string ResourceId {set;get;}

		public ConnectedApplication Resource {set;get;}

		public string Policy {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public bool IsDeleted {set;get;}

		public DateTime SystemModstamp {set;get;}
	}
}
