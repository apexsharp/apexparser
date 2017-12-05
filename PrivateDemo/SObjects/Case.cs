namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Case : SObject
	{
		public bool IsDeleted {set;get;}

		public string CaseNumber {set;get;}

		public string ContactId {set;get;}

		public Contact Contact {set;get;}

		public string AccountId {set;get;}

		public Account Account {set;get;}

		public string AssetId {set;get;}

		public Asset Asset {set;get;}

		public string BusinessHoursId {set;get;}

		public BusinessHours BusinessHours {set;get;}

		public string ParentId {set;get;}

		public Case Parent {set;get;}

		public string SuppliedName {set;get;}

		public string SuppliedEmail {set;get;}

		public string SuppliedPhone {set;get;}

		public string SuppliedCompany {set;get;}

		public string Type {set;get;}

		public string Status {set;get;}

		public string Reason {set;get;}

		public string Origin {set;get;}

		public string Subject {set;get;}

		public string Priority {set;get;}

		public string Description {set;get;}

		public bool IsClosed {set;get;}

		public DateTime ClosedDate {set;get;}

		public bool IsEscalated {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public bool IsClosedOnCreate {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public string ContactPhone {set;get;}

		public string ContactMobile {set;get;}

		public string ContactEmail {set;get;}

		public string ContactFax {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public string EngineeringReqNumber__c {set;get;}

		public string SLAViolation__c {set;get;}

		public string Product__c {set;get;}

		public string PotentialLiability__c {set;get;}
	}
}
