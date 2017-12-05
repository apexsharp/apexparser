namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class CampaignMember : SObject
	{
		public bool IsDeleted {set;get;}

		public string CampaignId {set;get;}

		public Campaign Campaign {set;get;}

		public string LeadId {set;get;}

		public Lead Lead {set;get;}

		public string ContactId {set;get;}

		public Contact Contact {set;get;}

		public string Status {set;get;}

		public bool HasResponded {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime FirstRespondedDate {set;get;}

		public string Salutation {set;get;}

		public string Name {set;get;}

		public string FirstName {set;get;}

		public string LastName {set;get;}

		public string Title {set;get;}

		public string Street {set;get;}

		public string City {set;get;}

		public string State {set;get;}

		public string PostalCode {set;get;}

		public string Country {set;get;}

		public string Email {set;get;}

		public string Phone {set;get;}

		public string Fax {set;get;}

		public string MobilePhone {set;get;}

		public string Description {set;get;}

		public bool DoNotCall {set;get;}

		public bool HasOptedOutOfEmail {set;get;}

		public bool HasOptedOutOfFax {set;get;}

		public string LeadSource {set;get;}

		public string CompanyOrAccount {set;get;}

		public string Type {set;get;}

		public string LeadOrContactId {set;get;}

		public string LeadOrContactOwnerId {set;get;}

		public Group LeadOrContactOwner {set;get;}
	}
}
