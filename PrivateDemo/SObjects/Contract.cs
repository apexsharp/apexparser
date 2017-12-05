namespace PrivateDemo.SObjects
{
	using Apex.System;
	using ApexSharpApi.ApexApi;

	public class Contract : SObject
	{
		public string AccountId {set;get;}

		public Account Account {set;get;}

		public string Pricebook2Id {set;get;}

		public Pricebook2 Pricebook2 {set;get;}

		public string OwnerExpirationNotice {set;get;}

		public DateTime StartDate {set;get;}

		public DateTime EndDate {set;get;}

		public string BillingStreet {set;get;}

		public string BillingCity {set;get;}

		public string BillingState {set;get;}

		public string BillingPostalCode {set;get;}

		public string BillingCountry {set;get;}

		public double BillingLatitude {set;get;}

		public double BillingLongitude {set;get;}

		public string BillingGeocodeAccuracy {set;get;}

		public Address BillingAddress {set;get;}

		public string ShippingStreet {set;get;}

		public string ShippingCity {set;get;}

		public string ShippingState {set;get;}

		public string ShippingPostalCode {set;get;}

		public string ShippingCountry {set;get;}

		public double ShippingLatitude {set;get;}

		public double ShippingLongitude {set;get;}

		public string ShippingGeocodeAccuracy {set;get;}

		public Address ShippingAddress {set;get;}

		public int ContractTerm {set;get;}

		public string OwnerId {set;get;}

		public User Owner {set;get;}

		public string Status {set;get;}

		public string CompanySignedId {set;get;}

		public User CompanySigned {set;get;}

		public DateTime CompanySignedDate {set;get;}

		public string CustomerSignedId {set;get;}

		public Contact CustomerSigned {set;get;}

		public string CustomerSignedTitle {set;get;}

		public DateTime CustomerSignedDate {set;get;}

		public string SpecialTerms {set;get;}

		public string ActivatedById {set;get;}

		public User ActivatedBy {set;get;}

		public DateTime ActivatedDate {set;get;}

		public string StatusCode {set;get;}

		public string Description {set;get;}

		public string Name {set;get;}

		public bool IsDeleted {set;get;}

		public string ContractNumber {set;get;}

		public DateTime LastApprovedDate {set;get;}

		public DateTime CreatedDate {set;get;}

		public string CreatedById {set;get;}

		public User CreatedBy {set;get;}

		public DateTime LastModifiedDate {set;get;}

		public string LastModifiedById {set;get;}

		public User LastModifiedBy {set;get;}

		public DateTime SystemModstamp {set;get;}

		public DateTime LastActivityDate {set;get;}

		public DateTime LastViewedDate {set;get;}

		public DateTime LastReferencedDate {set;get;}
	}
}
