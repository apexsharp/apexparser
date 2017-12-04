namespace ApexSharpApiDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class ContactCleanInfo : SObject
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
		public string ContactId {set;get;}
		public Contact Contact {set;get;}
		public DateTime LastMatchedDate {set;get;}
		public DateTime LastStatusChangedDate {set;get;}
		public string LastStatusChangedById {set;get;}
		public User LastStatusChangedBy {set;get;}
		public bool IsInactive {set;get;}
		public string FirstName {set;get;}
		public string LastName {set;get;}
		public string Email {set;get;}
		public string Phone {set;get;}
		public string Street {set;get;}
		public string City {set;get;}
		public string State {set;get;}
		public string PostalCode {set;get;}
		public string Country {set;get;}
		public double Latitude {set;get;}
		public double Longitude {set;get;}
		public string GeocodeAccuracy {set;get;}
		public Address Address {set;get;}
		public string Title {set;get;}
		public string ContactStatusDataDotCom {set;get;}
		public bool IsReviewedName {set;get;}
		public bool IsReviewedEmail {set;get;}
		public bool IsReviewedPhone {set;get;}
		public bool IsReviewedAddress {set;get;}
		public bool IsReviewedTitle {set;get;}
		public bool IsDifferentFirstName {set;get;}
		public bool IsDifferentLastName {set;get;}
		public bool IsDifferentEmail {set;get;}
		public bool IsDifferentPhone {set;get;}
		public bool IsDifferentStreet {set;get;}
		public bool IsDifferentCity {set;get;}
		public bool IsDifferentState {set;get;}
		public bool IsDifferentPostalCode {set;get;}
		public bool IsDifferentCountry {set;get;}
		public bool IsDifferentTitle {set;get;}
		public bool IsDifferentStateCode {set;get;}
		public bool IsDifferentCountryCode {set;get;}
		public bool CleanedByJob {set;get;}
		public bool CleanedByUser {set;get;}
		public bool IsFlaggedWrongName {set;get;}
		public bool IsFlaggedWrongEmail {set;get;}
		public bool IsFlaggedWrongPhone {set;get;}
		public bool IsFlaggedWrongAddress {set;get;}
		public bool IsFlaggedWrongTitle {set;get;}
		public string DataDotComId {set;get;}
	}
}
