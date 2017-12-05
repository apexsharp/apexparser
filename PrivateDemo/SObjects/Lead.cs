namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Lead : SObject
	{
		public bool IsDeleted {set;get;}

		public string MasterRecordId {set;get;}

		public Lead MasterRecord {set;get;}

		public string LastName {set;get;}

		public string FirstName {set;get;}

		public string Salutation {set;get;}

		public string Name {set;get;}

		public string Title {set;get;}

		public string Company {set;get;}

		public string Street {set;get;}

		public string City {set;get;}

		public string State {set;get;}

		public string PostalCode {set;get;}

		public string Country {set;get;}

		public double Latitude {set;get;}

		public double Longitude {set;get;}

		public string GeocodeAccuracy {set;get;}

		public Address Address {set;get;}

		public string Phone {set;get;}

		public string MobilePhone {set;get;}

		public string Fax {set;get;}

		public string Email {set;get;}

		public string Website {set;get;}

		public string PhotoUrl {set;get;}

		public string Description {set;get;}

		public string LeadSource {set;get;}

		public string Status {set;get;}

		public string Industry {set;get;}

		public string Rating {set;get;}

		public double AnnualRevenue {set;get;}

		public int NumberOfEmployees {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public bool HasOptedOutOfEmail {set;get;}

		public bool IsConverted {set;get;}

		public DateTime ConvertedDate {set;get;}

		public string ConvertedAccountId {set;get;}

		public Account ConvertedAccount {set;get;}

		public string ConvertedContactId {set;get;}

		public Contact ConvertedContact {set;get;}

		public string ConvertedOpportunityId {set;get;}

		public Opportunity ConvertedOpportunity {set;get;}

		public bool IsUnreadByOwner {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastActivityDate {set;get;}

		public bool DoNotCall {set;get;}

		public bool HasOptedOutOfFax {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}

		public DateTime LastTransferDate {set;get;}

		public string Jigsaw {set;get;}

		public string JigsawContactId {set;get;}

		public string CleanStatus {set;get;}

		public string CompanyDunsNumber {set;get;}

		public string DandbCompanyId {set;get;}

		public DandBCompany DandbCompany {set;get;}

		public string EmailBouncedReason {set;get;}

		public DateTime EmailBouncedDate {set;get;}

		public string SICCode__c {set;get;}

		public string ProductInterest__c {set;get;}

		public string Primary__c {set;get;}

		public string CurrentGenerators__c {set;get;}

		public double NumberofLocations__c {set;get;}
	}
}
